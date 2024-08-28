using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Models;
using GeekShopping.ProductApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers
{
    [Route( "api/v1/[controller]" )]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController( IProductRepository repository )
        {
            _repository = repository ?? throw new ArgumentException( nameof( repository ) );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll( )
        {
            var products = await _repository.FindAll( );

            return Ok( products );
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult> FindById( long id )
        {
            var product = await _repository.FindById( id );

            if ( product.Id <= 0 )
            {
                return NotFound( );
            }

            return Ok( product );
        }

        [HttpPost]
        public async Task<ActionResult> Create( ProductVO vo )
        {
            if ( vo == null )
            {
                return BadRequest( );
            }

            var product = await _repository.Create( vo );

            return Ok( product );
        }

        [HttpPut]
        public async Task<ActionResult> Update( ProductVO vo )
        {
            if ( vo == null )
            {
                return BadRequest( );
            }

            var product = await _repository.Update( vo );

            return Ok( product );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult> Update( long id )
        {
            var status = await _repository.Delete( id );

            if ( !status )
            {
                return BadRequest( );
            }

            return Ok( status );
        }
    }
}
