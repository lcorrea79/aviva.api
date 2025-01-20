using AutoMapper;
using AVIVA.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace AVIVA.Application.Mappings
{
    public class ItemsResolver<TSource, TDestination> : IValueResolver<Pagination<TSource>, Pagination<TDestination>, List<TDestination>>
    {
        public List<TDestination> Resolve(Pagination<TSource> source, Pagination<TDestination> destination, List<TDestination> destMember, ResolutionContext context)
        {
            return source.Result.Select(item => context.Mapper.Map<TDestination>(item)).ToList();
        }
    }
}
