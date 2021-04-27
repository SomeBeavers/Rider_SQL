using System;
using CoreMultiLib.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EF_BeaversLife.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<Club> IncludeGradesAndAnimal(this IQueryable<Club> query)
        {
            query = query
                    .Include(_ => _.Grades)
                    .ThenInclude(_ => _.Animal);
            return query;
        }
    }
}