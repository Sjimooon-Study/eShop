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

        DetailsLocomotiveDto GetDetails(int locomotiveId);
        Tuple<IQueryable<ListLocomotiveDto>, ushort, ushort> GetList(QueryOptions queryOptions);
        IQueryable<string> GetTags();
        EditLocomotiveDto GetEdit(int locomotiveId);

        int Edit(EditLocomotiveDto locomotiveDto);

        int Delete(params int[] locomotiveIds);
    }
}
