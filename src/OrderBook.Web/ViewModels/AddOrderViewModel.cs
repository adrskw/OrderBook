using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderBook.Web.Models;

namespace OrderBook.Web.ViewModels
{
    public class AddOrderViewModel
    {
        #region Customer Basic Info

        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string CustomerLastName { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string CustomerCompanyName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Podany adres email jest nieprawidłowy")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Numer telefonu")]
        [RegularExpression(@"^([+]?\d{1,2}[.\s-]?)?(\d{3}[.\s-]?){3}$", ErrorMessage = "Numer telefonu jest nieprawidłowy")]
        public string CustomerPhoneNumber { get; set; }

        [Display(Name = "Notatka o kliencie")]
        public string CustomerNote { get; set; }

        #endregion Customer Basic Info

        #region Customer Address

        [Required(ErrorMessage = "Ulica jest wymagana")]
        [Display(Name = "Ulica")]
        public string CustomerAddressStreet { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane")]
        [Display(Name = "Miasto")]
        public string CustomerAddressCity { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [Display(Name = "Kod pocztowy")]
        public string CustomerAddressZipCode { get; set; }

        [Required(ErrorMessage = "Państwo jest wymagane")]
        [Display(Name = "Państwo")]
        public string CustomerAddressCountry { get; set; }

        #endregion Customer Address

        #region Customer Invoice

        [Display(Name = "- faktura wymagana")]
        public bool CustomerInvoiceIsInvoiceRequired { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string CustomerInvoiceCompanyName { get; set; }

        [Display(Name = "NIP")]
        public string CustomerInvoiceCompanyTaxId { get; set; }

        [Display(Name = "Imię i nazwisko")]
        public string CustomerInvoiceNaturalPersonName { get; set; }

        [Display(Name = "Ulica")]
        public string CustomerInvoiceStreet { get; set; }

        [Display(Name = "Miasto")]
        public string CustomerInvoiceCity { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string CustomerInvoiceZipCode { get; set; }

        [Display(Name = "Państwo")]
        public string CustomerInvoiceCountry { get; set; }

        #endregion Customer Invoice

        #region Products

        public SelectList AvailableProductsSelectList { get; set; }

        [Display(Name = "Produkt")]
        public List<int> ProductIds { get; set; }

        [Display(Name = "Ilość")]
        public List<int> ProductQuantities { get; set; }

        #endregion Products

        #region Delivery Method

        public SelectList AvailableCarriersSelectList { get; set; }

        [Required(ErrorMessage = "Wybierz dostawcę")]
        [Display(Name = "Dostawca")]
        public DeliveryMethodCarrier DeliveryMethodCarrier { get; set; }

        [Display(Name = "Cena dostawy")]
        [Range(0, 99999999, ErrorMessage = "Cena dostawy musi być większa lub równa 0")]
        public decimal DeliveryMethodPrice { get; set; }

        [Display(Name = "- płatność przy odbiorze")]
        public bool DeliveryMethodIsCashOnDelivery { get; set; }

        [Display(Name = "Ilość paczek")]
        [Range(0, int.MaxValue, ErrorMessage = "Ilość paczek musi być większa lub równa 0")]
        public int DeliveryMethodNumberOfPackages { get; set; }

        #endregion Delivery Method

        #region Delivery Address

        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string DeliveryAddressFirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string DeliveryAddressLastName { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string DeliveryAddressCompanyName { get; set; }

        [Display(Name = "Numer telefonu")]
        [RegularExpression(@"^([+]?\d{1,2}[.\s-]?)?(\d{3}[.\s-]?){3}$", ErrorMessage = "Numer telefonu jest nieprawidłowy")]
        public string DeliveryAddressPhoneNumber { get; set; }

        [Required(ErrorMessage = "Ulica jest wymagana")]
        [Display(Name = "Ulica")]
        public string DeliveryAddressStreet { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane")]
        [Display(Name = "Miasto")]
        public string DeliveryAddressCity { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [Display(Name = "Kod pocztowy")]
        public string DeliveryAddressZipCode { get; set; }

        [Required(ErrorMessage = "Państwo jest wymagane")]
        [Display(Name = "Państwo")]
        public string DeliveryAddressCountry { get; set; }

        #endregion Delivery Address

        #region Order Info

        public SelectList OrderStatusSelectList { get; set; }

        [Required(ErrorMessage = "Wybierz status zamówienia")]
        [Display(Name = "Status zamówienia")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Notatka do zamówienia")]
        public string OrderNote { get; set; }

        #endregion Order Info
    }
}