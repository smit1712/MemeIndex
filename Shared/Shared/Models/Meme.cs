using System.Collections.Generic;

namespace Shared.Models
{
    public class Meme
    {
        public long Id { get; set; }
        public string OriginalId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<MemeCategory> MetaData { get; set; }
        public string Resource { get; set; }
        public int Rating { get; set; }

        public string Origin { get; set; }

        //Add unique ID based upon Resource, to limit dubbel entries
        public Meme()
        {

        }
        public Meme(string description, List<string> metaData, string resource, int rating, string origin, string originalId)
        {
            Description = description;
            Resource = resource;
            Rating = rating;
            Origin = origin;
            OriginalId = originalId;

            List<MemeCategory> meta = new List<MemeCategory>();
            metaData.ForEach(m => meta.Add(new MemeCategory(m)));
            MetaData = meta;
        }
    }
}
