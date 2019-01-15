using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Models;


namespace ToDoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            this._context = context;
            if(this._context.ProductItems.Count() == 0)
            {
                this._context.ProductItems.AddRange(new Product[]{
                new Product{
                            productId= 1,
                            productName= "Leaf Rake",
                            productCode= "GDN-0011",
                            releaseDate= "March 19, 2016",
                            description= "Leaf rake with 48-inch wooden handle.",
                            price= 19.95,
                            starRating= 3.2,
                            imageUrl= "https://openclipart.org/image/300px/svg_to_png/26215/Anonymous_Leaf_Rake.png"
                        },
                        new Product{
                            productId= 2,
                            productName= "Garden Cart",
                            productCode= "GDN-0023",
                            releaseDate= "March 18, 2016",
                            description= "15 gallon capacity rolling garden cart",
                            price= 32.99,
                            starRating= 4.2,
                            imageUrl= "https://openclipart.org/image/300px/svg_to_png/58471/garden_cart.png"
                        },
                        new Product{
                            productId= 5,
                            productName= "Hammer",
                            productCode= "TBX-0048",
                            releaseDate= "May 21, 2016",
                            description= "Curved claw steel hammer",
                            price= 8.9,
                            starRating= 4.8,
                            imageUrl= "https://openclipart.org/image/300px/svg_to_png/73/rejon_Hammer.png"
                        },
                        new Product{
                            productId= 8,
                            productName= "Saw",
                            productCode= "TBX-0022",
                            releaseDate= "May 15, 2016",
                            description= "15-inch steel blade hand saw",
                            price= 11.55,
                            starRating= 3.7,
                            imageUrl= "https://openclipart.org/image/300px/svg_to_png/27070/egore911_saw.png"
                        },
                        new Product{
                            productId= 10,
                            productName= "Video Game Controller",
                            productCode= "GMG-0042",
                            releaseDate= "October 15, 2015",
                            description= "Standard two-button video game controller",
                            price= 35.95,
                            starRating= 4.6,
                            imageUrl= "https://openclipart.org/image/300px/svg_to_png/120337/xbox-controller_01.png"
                        }
                });

                this._context.SaveChanges();
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
            => await this._context.ProductItems.ToListAsync();
    }
}