using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icecreamshop.ViewModels
{
    public class FlavourCreateViewModel
    {
        [Required(ErrorMessage = "Ange namn på smak")]
        [Display(Name = "Smak")]
        public string FlavourName { get; set; }

        [Required(ErrorMessage = "Ange en förteckning över smak")]
        [Display(Name = "Beskrivning")]
        public string FlavourDescription { get; set; }

        public IFormFile Photo { get; set; }
    }
}
