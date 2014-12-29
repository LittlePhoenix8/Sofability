using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofability.Models
{
    public class bookmarks
    {
        public int user_id { get; set; }
        public float red_percent { get; set; }
        public string tags { get; set; }
        public DateTime date_updated { get; set; }
        public bool favorite { get; set; }
        public int id { get; set; }
        public DateTime date_archived { get; set; }
        public DateTime date_opened { get; set; }
        public article articulo { get; set; } //*
        public string article_href { get; set; } //*
        public DateTime date_favorited { get; set; }
        public DateTime date_added { get; set; }
        public bool archived { get; set; }
    }

    public class article
    {
        public string domain { get; set; } //*
        public string title { get; set; } //*
        public string url { get; set; } //*
        public string lead_image_url { get; set; }
        public string author { get; set; }
        public string excerpt { get; set; }//*
        public string direction { get; set; }
        public int word_count { get; set; }
        public DateTime date_published { get; set; } //*
        public string dek { get; set; }
        public bool processed { get; set; }
        public string id { get; set; } //*
    }

    public class meta
    {
        public int num_pages { get; set; }
        public int page { get; set; }
        public int item_count_total { get; set; }
        public int item_count { get; set; }
    }

    public class condition
    {
        public DateTime opened_since { get; set; }
        public DateTime added_until { get; set; }
        public string domain { get; set; }
        public DateTime updated_until { get; set; }
        public string tags { get; set; }
        public int archive { get; set; }
        public DateTime archived_until { get; set; }
        public int favorite { get; set; }
        public DateTime opened_until { get; set; }
        public DateTime archived_since { get; set; }
        public DateTime favirited_since { get; set; }
        public DateTime updated_since { get; set; }
        public string exclude_accessibility { get; set; }
        public int per_page { get; set; }
        public DateTime favorite_until { get; set; }
        public string order { get; set; }
        public int only_deleted { get; set; }
        public int page { get; set; }
        public DateTime added_since { get; set; }
        public string user { get; set; }
    }

    public class vistaArticulo
    {
        public string direction { get; set; }
        public string next_page_href { get; set; }
        public string author { get; set; }
        public string url { get; set; }
        public string lead_image_url { get; set; }
        public string title { get; set; }
        public string excerpt { get; set; }
        public string domain { get; set; }
        public int word_count { get; set; }
        public string content { get; set; }
        public DateTime date_published { get; set; }
        public string dek { get; set; }
        public bool processed { get; set; }
        public int content_size { get; set; }
        public string short_url { get; set; }
        public string id { get; set; }
    }
}
