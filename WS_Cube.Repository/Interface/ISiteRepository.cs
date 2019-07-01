using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.ViewModel;

namespace WS_Cube.Repository.Interface
{
    public interface ISiteRepository
    {
        Task<IEnumerable<SiteViewModel>> GetSites(int userID);
    }
}
