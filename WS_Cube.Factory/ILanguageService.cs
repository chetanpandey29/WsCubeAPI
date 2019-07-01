using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS_Cube.ViewModel;

namespace WS_Cube.Factory
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageViewModel>> GetLanguages();
    }
}
