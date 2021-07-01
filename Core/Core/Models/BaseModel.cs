using MvvmHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class BaseModel : ObservableObject
    {
        #region Fields
        Guid id;
        DateTimeOffset? updatedAt;
        bool deleted;
        #endregion

        [Key]
        public virtual Guid Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        public DateTimeOffset? UpdatedAt
        {
            get => updatedAt;
            set => SetProperty(ref updatedAt, value);
        }
        public bool Deleted
        {
            get => deleted;
            set => SetProperty(ref deleted, value);
        }

        public BaseModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
