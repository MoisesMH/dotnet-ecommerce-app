using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Product : BaseEntity
    {
        // Get or Set this property outside of this particular class
        // Now id is inherited from BaseEntity class
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        // One to One relationship for tables
        // Instanting it help us recover all of its attributes defined in its constructor
        // When the migration is created, it'll help EF know that 
        // the product has a relationship with ProductType, same case for ProductBrand
        // So it's going to use that information to set up the relationships for us, 
        // as well as the foreign keys
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }