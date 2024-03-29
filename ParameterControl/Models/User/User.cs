﻿using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.User
{
    public class User : GeneralData
    {
        public int Code { get; set; }
        [Required(ErrorMessage = "El campo usuario usuario es requerido")]
        public string User_ { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo email usuario es requerido")]
        [EmailAddress(ErrorMessage = "Este no es un correo valido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo nombre de usuario es requerido")]
        public string Name { get; set; } = string.Empty;
        public bool FirstAccess { get; set; }
        public string Password { get; set; }
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "El campo rol es requerido")]
        public int RolCode { get; set; }
        public string RolName { get; set; } = string.Empty;
    }
}
