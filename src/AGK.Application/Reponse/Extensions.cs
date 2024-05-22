using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGK.Application.Reponse;
internal static class Extensions
{
	//internal static async Task<Page<T>> AsPageAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken) {
	//	var totalItems = await query.CountAsync(cancellationToken);

	//	if(pageNumber < 1) {
	//		throw new ArgumentOutOfRangeException(nameof(pageNumber), "Error: Required pageNumber > 0");
	//	}

	//	if(pageSize > 0) {
	//		query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
	//	}

	//	return new Page<T>(pageNumber, pageSize, totalItems, await query.ToListAsync(cancellationToken));
	//}

}
