using CatalogAPI.Repositories;
using GraphQL;
using GraphQL.Types;

namespace CatalogAPI.Queries
{
    public class CatalogGQLQuery:ObjectGraphType
    {
        public CatalogGQLQuery(ICatalogRepository CatalogRepository)
        {

            Name = "AllCatalogQuery";
            //get all catalogs
            Field<ListGraphType<CatalogQueryType>>(
              "catalogs",
              resolve: context => CatalogRepository.GetAllCatalog()
          );

            //get catalog by id
            Field<CatalogQueryType>(
               "CatalogByIdQuery",
               arguments: new QueryArguments(new QueryArgument<LongGraphType> { Name = "CatalogId" }),
               resolve: context => CatalogRepository.GetCatalogById(context.GetArgument<long>("CatalogId"))

               );


        }
    }
}
