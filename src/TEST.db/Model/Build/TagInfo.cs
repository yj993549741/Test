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
	public partial class TagInfo {
		#region fields
		private int? _Id;
		private string _Name;
		#endregion

		public TagInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Tag(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static TagInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，TagInfo：{stringify}");
			TagInfo item = new TagInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TagInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (!__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
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
			set { _Id = value; }
		}

		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}

		private List<Goods_tagInfo> _obj_goods_tags;
		public List<Goods_tagInfo> Obj_goods_tags => _obj_goods_tags ?? (_obj_goods_tags = BLL.Goods_tag.SelectByTag_id(_Id).Limit(500).ToList());
		#endregion

		public TEST.DAL.Tag.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Tag.UpdateDiy(new List<TagInfo> { this });

		#region sync methods

		public TagInfo Save() {
			if (this.Id != null) {
				if (BLL.Tag.Update(this) == 0) return BLL.Tag.Insert(this);
				return this;
			}
			return BLL.Tag.Insert(this);
		}
		public Goods_tagInfo AddGoods(int? Goods_id) => AddGoods(new Goods_tagInfo {
			Goods_id = Goods_id});
		public Goods_tagInfo AddGoods(Goods_tagInfo item) {
			item.Tag_id = this.Id;
			return BLL.Goods_tag.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<TagInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Tag.UpdateAsync(this) == 0) return await BLL.Tag.InsertAsync(this);
				return this;
			}
			return await BLL.Tag.InsertAsync(this);
		}
		async public Task<Goods_tagInfo> AddGoodsAsync(int? Goods_id) => await AddGoodsAsync(new Goods_tagInfo {
			Goods_id = Goods_id});
		async public Task<Goods_tagInfo> AddGoodsAsync(Goods_tagInfo item) {
			item.Tag_id = this.Id;
			return await BLL.Goods_tag.InsertAsync(item);
		}

		#endregion
	}
}
