#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Needed for the [NotMapped] functionality
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefNDishes.Models;

public class Dish
{
    [Key]
    public int DishId {get;set;}

    [Required]
    public string DishName {get;set;}

    [Required]
    [Range(0, Int32.MaxValue)]
    public int Calories {get;set;}

    [Required]
    [MinLength(8)]
    public string Description {get;set;}

    [Required]
    [Range(1, 6)]
    public int Tastiness {get;set;}

    [Required]
    public int ChefId {get;set;}
    
    // Navigation property for related Chef object
    public Chef? Chef {get;set;}


    [Required]
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}