using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Omf.ReviewManagementService.DomainModel
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public string Rating { get; set; }
        public string Comments { get; set; }
        public string RestaurantId { get; set; }
        public Guid UserId { get; set; }        
    }
}