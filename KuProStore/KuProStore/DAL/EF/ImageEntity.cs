//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KuProStore.DAL.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageEntity
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
    
        public virtual ProductEntity Product { get; set; }
    }
}
