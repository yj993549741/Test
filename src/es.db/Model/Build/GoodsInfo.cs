using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace es.Model {
	[JsonObject(MemberSerialization.OptIn)]
	public partial class GoodsInfo {
		#region fields
		private int? _Id;
		private Goods_tagInfo _obj_goods_tag;
		private int? _Category_id;
		private CategoryInfo _obj_category;
		private string _Content;
		private DateTime? _Create_time;
		private string _Imgs;
		private int? _Stock;
		private string _Title;
		private DateTime? _Update_time;
		#endregion

		public GoodsInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Goods(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Category_id == null ? "null" : _Category_id.ToString(), "|",
				_Content == null ? "null" : _Content.Replace("|", StringifySplit), "|",
				_Create_time == null ? "null" : _Create_time.Value.Ticks.ToString(), "|",
				_Imgs == null ? "null" : _Imgs.Replace("|", StringifySplit), "|",
				_Stock == null ? "null" : _Stock.ToString(), "|",
				_Title == null ? "null" : _Title.Replace("|", StringifySplit), "|",
				_Update_time == null ? "null" : _Update_time.Value.Ticks.ToString());
		}
		public static GoodsInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 8, StringSplitOptions.None);
			if (ret.Length != 8) throw new Exception($"格式不正确，GoodsInfo：{stringify}");
			GoodsInfo item = new GoodsInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Category_id = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.Content = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.Create_time = new DateTime(long.Parse(ret[3]));
			if (string.Compare("null", ret[4]) != 0) item.Imgs = ret[4].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[5]) != 0) item.Stock = int.Parse(ret[5]);
			if (string.Compare("null", ret[6]) != 0) item.Title = ret[6].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[7]) != 0) item.Update_time = new DateTime(long.Parse(ret[7]));
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(GoodsInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Id") ? string.Empty : string.Format(", Id : {0}", Id == null ? "null" : Id.ToString()), 
				__jsonIgnore.ContainsKey("Category_id") ? string.Empty : string.Format(", Category_id : {0}", Category_id == null ? "null" : Category_id.ToString()), 
				__jsonIgnore.ContainsKey("Content") ? string.Empty : string.Format(", Content : {0}", Content == null ? "null" : string.Format("'{0}'", Content.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Create_time") ? string.Empty : string.Format(", Create_time : {0}", Create_time == null ? "null" : string.Concat("", Create_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("Imgs") ? string.Empty : string.Format(", Imgs : {0}", Imgs == null ? "null" : string.Format("'{0}'", Imgs.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Stock") ? string.Empty : string.Format(", Stock : {0}", Stock == null ? "null" : Stock.ToString()), 
				__jsonIgnore.ContainsKey("Title") ? string.Empty : string.Format(", Title : {0}", Title == null ? "null" : string.Format("'{0}'", Title.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Update_time") ? string.Empty : string.Format(", Update_time : {0}", Update_time == null ? "null" : string.Concat("", Update_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (!__jsonIgnore.ContainsKey("Category_id")) ht["Category_id"] = Category_id;
			if (!__jsonIgnore.ContainsKey("Content")) ht["Content"] = Content;
			if (!__jsonIgnore.ContainsKey("Create_time")) ht["Create_time"] = Create_time;
			if (!__jsonIgnore.ContainsKey("Imgs")) ht["Imgs"] = Imgs;
			if (!__jsonIgnore.ContainsKey("Stock")) ht["Stock"] = Stock;
			if (!__jsonIgnore.ContainsKey("Title")) ht["Title"] = Title;
			if (!__jsonIgnore.ContainsKey("Update_time")) ht["Update_time"] = Update_time;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? Id {
			get { return _Id; }
			set {
				if (_Id != value) _obj_goods_tag = null;
				_Id = value;
			}
		}

		public Goods_tagInfo Obj_goods_tag {
			get {
				if (_obj_goods_tag == null && _Id != null) _obj_goods_tag = es.BLL.Goods_tag.GetItem(_Id.Value);
				return _obj_goods_tag;
			}
			internal set { _obj_goods_tag = value; }
		}

		[JsonProperty] public int? Category_id {
			get { return _Category_id; }
			set {
				if (_Category_id != value) _obj_category = null;
				_Category_id = value;
			}
		}

		public CategoryInfo Obj_category {
			get {
				if (_obj_category == null && _Category_id != null) _obj_category = es.BLL.Category.GetItem(_Category_id.Value);
				return _obj_category;
			}
			internal set { _obj_category = value; }
		}

		[JsonProperty] public string Content {
			get { return _Content; }
			set { _Content = value; }
		}

		[JsonProperty] public DateTime? Create_time {
			get { return _Create_time; }
			set { _Create_time = value; }
		}

		[JsonProperty] public string Imgs {
			get { return _Imgs; }
			set { _Imgs = value; }
		}

		[JsonProperty] public int? Stock {
			get { return _Stock; }
			set { _Stock = value; }
		}

		[JsonProperty] public string Title {
			get { return _Title; }
			set { _Title = value; }
		}

		[JsonProperty] public DateTime? Update_time {
			get { return _Update_time; }
			set { _Update_time = value; }
		}

		private List<CommentInfo> _obj_comments;
		public List<CommentInfo> Obj_comments => _obj_comments ?? (_obj_comments = BLL.Comment.SelectByGoods_id(_Id).Limit(500).ToList());
		#endregion

		public es.DAL.Goods.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Goods.UpdateDiy(new List<GoodsInfo> { this });

		#region sync methods

		public GoodsInfo Save() {
			this.Update_time = DateTime.Now;
			if (this.Id != null) {
				if (BLL.Goods.Update(this) == 0) return BLL.Goods.Insert(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return BLL.Goods.Insert(this);
		}
		public CommentInfo AddComment(int? Id, string Content, string Nickname) => AddComment(new CommentInfo {
			Id = Id, 
			Content = Content, 
			Nickname = Nickname});
		public CommentInfo AddComment(CommentInfo item) {
			item.Goods_id = this.Id;
			return BLL.Comment.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<GoodsInfo> SaveAsync() {
			this.Update_time = DateTime.Now;
			if (this.Id != null) {
				if (await BLL.Goods.UpdateAsync(this) == 0) return await BLL.Goods.InsertAsync(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return await BLL.Goods.InsertAsync(this);
		}
		async public Task<CommentInfo> AddCommentAsync(int? Id, string Content, string Nickname) => await AddCommentAsync(new CommentInfo {
			Id = Id, 
			Content = Content, 
			Nickname = Nickname});
		async public Task<CommentInfo> AddCommentAsync(CommentInfo item) {
			item.Goods_id = this.Id;
			return await BLL.Comment.InsertAsync(item);
		}

		#endregion
	}
}
