using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderBook.Web.Models;
using OrderBook.Web.Models.DataTables;
using OrderBook.Web.Repositories;
using OrderBook.Web.Utilities;
using OrderBook.Web.ViewModels;

namespace OrderBook.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IReadOnlyDictionary<OrderStatus, string> orderStatusNames = new Dictionary<OrderStatus, string>()
        {
            { OrderStatus.Cancelled, "anulowane" },
            { OrderStatus.Unpaid, "nieopłacone" },
            { OrderStatus.Paid, "opłacone" },
            { OrderStatus.Processing, "w realizacji" },
            { OrderStatus.ReadyForShipment, "do wysłania" },
            { OrderStatus.Sent, "wysłane" },
            { OrderStatus.Returned, "zwrócone" },
            { OrderStatus.Refunded, "zwrot środków" }
        };

        private readonly IReadOnlyDictionary<DeliveryMethodCarrier, string> carrierNames = new Dictionary<DeliveryMethodCarrier, string>()
        {
            { DeliveryMethodCarrier.GLS, "GLS" },
            { DeliveryMethodCarrier.DHL, "DHL" },
            { DeliveryMethodCarrier.DPD, "DPD" }
        };

        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;

        public OrdersController(IOrderRepository orderRepository,
                                IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        #region Listing Orders

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sent()
        {
            var model = new ListOrdersViewModel()
            {
                StatusOfListedOrders = OrderStatus.Sent
            };

            return View("Index", model);
        }

        public IActionResult ReadyForShipment()
        {
            var model = new ListOrdersViewModel()
            {
                StatusOfListedOrders = OrderStatus.ReadyForShipment
            };

            return View("Index", model);
        }

        public IActionResult Processing()
        {
            var model = new ListOrdersViewModel()
            {
                StatusOfListedOrders = OrderStatus.Processing
            };

            return View("Index", model);
        }

        public IActionResult Paid()
        {
            var model = new ListOrdersViewModel()
            {
                StatusOfListedOrders = OrderStatus.Paid
            };

            return View("Index", model);
        }

        public IActionResult Unpaid()
        {
            var model = new ListOrdersViewModel()
            {
                StatusOfListedOrders = OrderStatus.Unpaid
            };

            return View("Index", model);
        }

        public IActionResult Cancelled()
        {
            var model = new ListOrdersViewModel()
            {
                StatusOfListedOrders = OrderStatus.Cancelled
            };

            return View("Index", model);
        }

        #endregion Listing Orders

        [HttpGet]
        public IActionResult Add()
        {
            var products = productRepository.GetAllProducts();

            var model = new AddOrderViewModel()
            {
                OrderStatusSelectList = new SelectList(orderStatusNames, "Key", "Value"),
                AvailableCarriersSelectList = new SelectList(carrierNames, "Key", "Value"),
                AvailableProductsSelectList = new SelectList(products, "Id", "Name")
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderDetails = new List<OrderDetail>();
                decimal totalToPay = model.DeliveryMethodPrice;

                for (int i = 0; i < model.ProductIds.Count; i++)
                {
                    var product = productRepository.GetById(model.ProductIds[i]);

                    if (product == null && model.ProductQuantities[i] <= 0)
                    {
                        ViewBag.Title = "Dodaj nowe zamówienie";
                        ViewBag.ErrorMessage = "Wybrano nieprawidłowy produkt lub jego ilość";

                        return View("Error");
                    }
                    else
                    {
                        var orderDetail = new OrderDetail()
                        {
                            Product = product,
                            UnitPrice = product.Price,
                            Quantity = model.ProductQuantities[i]
                        };

                        orderDetails.Add(orderDetail);
                        totalToPay += product.Price * model.ProductQuantities[i];
                    }
                }

                var deliveryAddress = new DeliveryAddress
                {
                    FirstName = model.DeliveryAddressFirstName,
                    LastName = model.DeliveryAddressLastName,
                    CompanyName = model.DeliveryAddressCompanyName,
                    Street = model.DeliveryAddressStreet,
                    City = model.DeliveryAddressCity,
                    ZipCode = model.DeliveryAddressZipCode,
                    Country = model.DeliveryAddressCountry,
                    PhoneNumber = model.DeliveryAddressPhoneNumber
                };

                var order = new Order()
                {
                    Type = OrderType.Manual,
                    Customer = new Customer
                    {
                        FirstName = model.CustomerFirstName,
                        LastName = model.CustomerLastName,
                        CompanyName = model.CustomerCompanyName,
                        Email = model.CustomerEmail,
                        PhoneNumber = model.CustomerPhoneNumber,
                        Address = new Address()
                        {
                            Street = model.CustomerAddressStreet,
                            City = model.CustomerAddressCity,
                            ZipCode = model.CustomerAddressZipCode,
                            Country = model.CustomerAddressCountry
                        },
                        DeliveryAddresses = new List<DeliveryAddress>() {
                            deliveryAddress
                        },
                        Invoice = new Invoice()
                        {
                            CompanyName = model.CustomerInvoiceCompanyName,
                            CompanyTaxId = model.CustomerInvoiceCompanyTaxId,
                            NaturalPersonName = model.CustomerInvoiceNaturalPersonName,
                            Street = model.CustomerInvoiceStreet,
                            City = model.CustomerInvoiceCity,
                            ZipCode = model.CustomerInvoiceZipCode,
                            Country = model.CustomerInvoiceCountry
                        },
                        Note = model.CustomerNote
                    },
                    Products = orderDetails,
                    DeliveryMethod = new DeliveryMethod()
                    {
                        Carrier = model.DeliveryMethodCarrier,
                        Price = model.DeliveryMethodPrice,
                        IsCashOnDelivery = model.DeliveryMethodIsCashOnDelivery
                    },
                    NumberOfPackages = model.DeliveryMethodNumberOfPackages,
                    SelectedDeliveryAddress = deliveryAddress,
                    TotalToPay = totalToPay,
                    IsInvoiceRequired = model.CustomerInvoiceIsInvoiceRequired,
                    Status = model.OrderStatus,
                    OrderDate = DateTime.Now,
                    Note = model.OrderNote
                };

                orderRepository.Add(order);
                orderRepository.Save();

                return RedirectToAction("Index");
            }
            else
            {
                var products = productRepository.GetAllProducts();

                model = new AddOrderViewModel()
                {
                    OrderStatusSelectList = new SelectList(orderStatusNames, "Key", "Value"),
                    AvailableCarriersSelectList = new SelectList(carrierNames, "Key", "Value"),
                    AvailableProductsSelectList = new SelectList(products, "Id", "Name")
                };
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = orderRepository.GetById(id);

            if (order == null)
            {
                ViewBag.Title = "Szczegóły zamówienia";
                ViewBag.ErrorMessage = "Nie znaleziono zamówienia o danym identyfikatorze";

                return View("Error");
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = orderRepository.GetById(id);

            if (order == null)
            {
                ViewBag.Title = "Usuń zamówienie";
                ViewBag.ErrorMessage = "Nie znaleziono zamówienia o danym identyfikatorze";

                return View("Error");
            }

            orderRepository.Delete(order.Id);
            orderRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult LoadTableData(DataTablesOrdersTableAjaxPost model)
        {
            var searchResult = DataTablesHelper.OrdersTable_Search(orderRepository, model,
                out int filteredResultsCount,
                out int totalResultsCount);
            return Json(new
            {
                draw = model.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = searchResult
            });
        }
    }
}