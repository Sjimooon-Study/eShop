using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.LocomotiveService
{
    public interface ILocomotiveService
    {
        int Add(AddLocomotiveDto locomotive, bool saveChanges = true);
        int Add(ICollection<AddLocomotiveDto> locomotives);

        DetailsLocomotiveDto GetDetailsLocomotive(int locomotiveId);
        IQueryable<ListLocomotiveDto> GetListLocomotives(QueryOptions queryOptions);

        int Edit(EditLocomotiveDto locomotiveDto);

        int Delete(params int[] locomotiveIds);
    }
}
