using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TEST.Model {
	[JsonObject(MemberSerialization.OptIn)]
	public partial class Goods_tagInfo {
		#region fields
		private int? _Goods_id;
		private int? _Tag_id;
		private TagInfo _obj_tag;
		#endregion

		public Goods_tagInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Goods_tag(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Goods_id == null ? "null" : _Goods_id.ToString(), "|",
				_Tag_id == null ? "null" : _Tag_id.ToString());
		}
		public static Goods_tagInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，Goods_tagInfo：{stringify}");
			Goods_tagInfo item = new Goods_tagInfo();
			if (string.Compare("null", ret[0]) != 0) item.Goods_id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Tag_id = int.Parse(ret[1]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Goods_tagInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Goods_id") ? string.Empty : string.Format(", Goods_id : {0}", Goods_id == null ? "null" : Goods_id.ToString()), 
				__jsonIgnore.ContainsKey("Tag_id") ? string.Empty : string.Format(", Tag_id : {0}", Tag_id == null ? "null" : Tag_id.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Goods_id")) ht["Goods_id"] = Goods_id;
			if (!__jsonIgnore.ContainsKey("Tag_id")) ht["Tag_id"] = Tag_id;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? Goods_id {
			get { return _Goods_id; }
			set { _Goods_id = value; }
		}

		[JsonProperty] public int? Tag_id {
			get { return _Tag_id; }
			set {
				if (_Tag_id != value) _obj_tag = null;
				_Tag_id = value;
			}
		}

		public TagInfo Obj_tag {
			get {
				if (_obj_tag == null && _Tag_id != null) _obj_tag = TEST.BLL.Tag.GetItem(_Tag_id.Value);
				return _obj_tag;
			}
			internal set { _obj_tag = value; }
		}

		private GoodsInfo _obj_goods;
		public GoodsInfo Obj_goods {
			get { return _obj_goods ?? (_Goods_id == null ? null : (_obj_goods = BLL.Goods.GetItem(_Goods_id.Value))); }
			internal set { _obj_goods = value; }
		}
		#endregion

		public TEST.DAL.Goods_tag.SqlUpdateBuild UpdateDiy => _Goods_id == null ? null : BLL.Goods_tag.UpdateDiy(new List<Goods_tagInfo> { this });

		#region sync methods

		public Goods_tagInfo Save() {
			if (this.Goods_id != null) {
				if (BLL.Goods_tag.Update(this) == 0) return BLL.Goods_tag.Insert(this);
				return this;
			}
			return BLL.Goods_tag.Insert(this);
		}
		public GoodsInfo AddGoods(CategoryInfo Category, string Content, string Imgs, int? Stock, string Title) => AddGoods(Category.Id, Content, Imgs, Stock, Title);
		public GoodsInfo AddGoods(int? Category_id, string Content, string Imgs, int? Stock, string Title) => AddGoods(new GoodsInfo {
			Category_id = Category_id, 
			Content = Content, 
			Imgs = Imgs, 
			Stock = Stock, 
			Title = Title});
		public GoodsInfo AddGoods(GoodsInfo item) {
			item.Id = this.Goods_id;
			return BLL.Goods.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<Goods_tagInfo> SaveAsync() {
			if (this.Goods_id != null) {
				if (await BLL.Goods_tag.UpdateAsync(this) == 0) return await BLL.Goods_tag.InsertAsync(this);
				return this;
			}
			return await BLL.Goods_tag.InsertAsync(this);
		}
		async public Task<GoodsInfo> AddGoodsAsync(CategoryInfo Category, string Content, string Imgs, int? Stock, string Title) => await AddGoodsAsync(Category.Id, Content, Imgs, Stock, Title);
		async public Task<GoodsInfo> AddGoodsAsync(int? Category_id, string Content, string Imgs, int? Stock, string Title) => await AddGoodsAsync(new GoodsInfo {
			Category_id = Category_id, 
			Content = Content, 
			Imgs = Imgs, 
			Stock = Stock, 
			Title = Title});
		async public Task<GoodsInfo> AddGoodsAsync(GoodsInfo item) {
			item.Id = this.Goods_id;
			return await BLL.Goods.InsertAsync(item);
		}

		#endregion
	}
}
