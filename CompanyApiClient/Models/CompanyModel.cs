using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CompanyAPISimple.Models
{
    /// <summary>
    /// Defines the company type
    /// </summary>
    public class CompanyModel
    {
        /// <summary>
        /// Companies International Securities Identification Number(ISIN)
        /// </summary>       
        public int Id { get; set; }

        /// <summary>
        /// Companies International Securities Identification Number(ISIN)
        /// </summary>
        [Required]
        // Valid ISIN matches defined format
        // characters. Use custom error.
        [RegularExpression(@"^[A-Z]{2}[A-Z0-9]{9}[0-9]$",
         ErrorMessage = "ISIN Invalid")]
        [Display(Name = "ISIN")]
        public string Isin { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        /// <summary>
        /// Exchange traded on
        /// </summary>
        [Required]
        [Display(Name = "Exchange")]
        public string exchange { get; set; }

        /// <summary>
        /// Ticker on the exchange
        /// </summary>
        [Required]
        [Display(Name = "Stock Ticker")]
        public string ticker { get; set; }

        /// <summary>
        /// company URI
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Display(Name = "URL")]
        public string website { get; set; }

        /// <summary>
        /// Equality comparitor so company objects can be compared for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj is CompanyModel company)
            {
                if (
                    ((this.name == null && company.ticker == null) || (this.name != null && this.name.Equals(company.name))) &&
                    ((this.Isin == null && company.Isin == null) || (this.Isin != null && this.Isin.Equals(company.Isin))) &&
                    ((this.exchange == null && company.exchange == null) || (this.exchange != null && this.exchange.Equals(company.exchange))) &&
                    ((this.ticker == null && company.ticker == null) || (this.ticker != null && this.ticker.Equals(company.ticker))) &&
                    ((this.website == null && company.website == null) || (this.website != null && this.website.Equals(company.website)))
                   )
                {
                    areEqual = true;
                }
            }

            return areEqual;
        }

        public override int GetHashCode()
        {
            var hashCode = -2145851079;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Isin);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(exchange);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ticker);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(website);
            return hashCode;
        }

    }
}