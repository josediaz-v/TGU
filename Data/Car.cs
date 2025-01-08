using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TGU.Data
{
    public class Car
    {
        [Required(ErrorMessage = "VIN is required.")]
        [Key]  // Specify that this property is the primary key
        [Display(Name = "VIN Number")]
        public string Vin { get; set; } = string.Empty;  // Keep vin as the primary key
        public string OwnerEmail { get; set; } = string.Empty; // Link to the user ID from AspNetUsers

        [Required(ErrorMessage = "Plate is required.")]
        public string Plate { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = string.Empty;

        [JsonPropertyName("class")]
        public string? CarClass { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Transmission is required.")]
        public string Transmission { get; set; } = string.Empty;
        public string? Drive { get; set; }
        public int? Cylinders { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AssignedTo { get; set; }
        public List<string> PictureUrls { get; set; } = new List<string>();
    }
}
