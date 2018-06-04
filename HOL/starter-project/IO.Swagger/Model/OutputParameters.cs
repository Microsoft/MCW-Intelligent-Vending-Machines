/* 
 * apiPredictPurchasePrice
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// OutputParameters
    /// </summary>
    [DataContract]
    public partial class OutputParameters :  IEquatable<OutputParameters>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputParameters" /> class.
        /// </summary>
        /// <param name="PurchasePrice">numeric.</param>
        public OutputParameters(decimal? PurchasePrice = default(decimal?))
        {
            this.PurchasePrice = PurchasePrice;
        }
        
        /// <summary>
        /// numeric
        /// </summary>
        /// <value>numeric</value>
        [DataMember(Name="purchasePrice", EmitDefaultValue=false)]
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OutputParameters {\n");
            sb.Append("  PurchasePrice: ").Append(PurchasePrice).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as OutputParameters);
        }

        /// <summary>
        /// Returns true if OutputParameters instances are equal
        /// </summary>
        /// <param name="other">Instance of OutputParameters to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OutputParameters other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.PurchasePrice == other.PurchasePrice ||
                    this.PurchasePrice != null &&
                    this.PurchasePrice.Equals(other.PurchasePrice)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.PurchasePrice != null)
                    hash = hash * 59 + this.PurchasePrice.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
