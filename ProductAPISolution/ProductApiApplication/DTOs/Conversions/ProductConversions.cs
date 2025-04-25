using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApiDomain.Entites;

namespace ProductApiApplication.DTOs.Conversions
{
   public static class ProductConversions
    {
        public static Product ToEntity(ProductDTO product) => new()
        { Id = product.Id, 
         Name = product.Name,
         Quantity = product.Quantity,
         Price = product.Price,
        
        
        
        };

        public static (ProductDTO? , IEnumerable<ProductDTO>?) FromEntity(Product product , IEnumerable<Product>? products)
        {
            // return single
            if(product is not null || products is  null)
            {
                var singleProduct = new ProductDTO
                (
                    product!.Id,
                    product.Name!,
                    product.Price,
                    product.Quantity
                    
                
                );
                return (singleProduct, null);
            }

            // return List
            if (products is not null || product is null)
            {
                var _products = products!.Select(p =>
                
                    new ProductDTO(p.Id, p.Name, p.Price, p.Quantity)).ToList();
                    

                return (null , _products);
                
            }
            return(null , null);
        }

    }
}
