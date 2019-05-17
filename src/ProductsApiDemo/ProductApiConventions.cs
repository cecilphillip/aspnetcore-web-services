using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ProductsApiDemo
{
    public static class ProductApiConventions
    {        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(){ }


        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(204)]
        public static void Delete([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)][ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id) { }

    }
}
