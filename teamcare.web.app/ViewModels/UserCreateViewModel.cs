using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.web.app.ViewModels
{
    public class UserCreateViewModel
    {
        public UserModel User { get; set; }
        public string TempFileId { get; set; }

    }
}
