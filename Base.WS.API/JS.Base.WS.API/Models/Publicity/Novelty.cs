using JS.Base.WS.API.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JS.Base.WS.API.Models.Publicity
{
    public class Novelty: Audit
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public int NoveltyTypeId { get; set; }
        public string Img { get; set; }
        public string ImgPath { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsPublic { get; set; }
        public string ContenTypeShort { get; set; }
        public string ContenTypeLong { get; set; }


        [ForeignKey("NoveltyTypeId")]
        public virtual NoveltyType NoveltyType { get; set; }
    }
}