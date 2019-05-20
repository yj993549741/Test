using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TEST.Model;

namespace TEST.BLL {

	public partial class Category {

		internal static readonly TEST.DAL.Category dal = new TEST.DAL.Category();
		internal static readonly int itemCacheTimeout;

		static Category() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Category"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static CategoryInfo Delete(int Id) {
			var item = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<CategoryInfo> DeleteByParent_id(int Parent_id) {
			var items = dal.DeleteByParent_id(Parent_id);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return items;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			Parent_id, 
			Create_time, 
			Name
		}
		#endregion

		public static int Update(CategoryInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(CategoryInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static TEST.DAL.Category.SqlUpdateBuild UpdateDiy(int Id) => new TEST.DAL.Category.SqlUpdateBuild(new List<CategoryInfo> { new CategoryInfo { Id = Id } }, false);
		public static TEST.DAL.Category.SqlUpdateBuild UpdateDiy(List<CategoryInfo> dataSource) => new TEST.DAL.Category.SqlUpdateBuild(dataSource, true);
		public static TEST.DAL.Category.SqlUpdateBuild UpdateDiyDangerous => new TEST.DAL.Category.SqlUpdateBuild();

		public static CategoryInfo Insert(int? Parent_id, string Name) {
			return Insert(new CategoryInfo {
				Parent_id = Parent_id, 
				Name = Name});
		}
		public static CategoryInfo Insert(CategoryInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<CategoryInfo> Insert(IEnumerable<CategoryInfo> items) {
			foreach (var item in items) if (item != null && item.Create_time == null) item.Create_time = DateTime.Now;
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(CategoryInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<CategoryInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("TEST_BLL:Category:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static CategoryInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("TEST_BLL:Category:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne());

		public static List<CategoryInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<CategoryInfo> GetItemsByParent_id(params int?[] Parent_id) => Select.WhereParent_id(Parent_id).ToList();
		public static List<CategoryInfo> GetItemsByParent_id(int?[] Parent_id, int limit) => Select.WhereParent_id(Parent_id).Limit(limit).ToList();
		public static SelectBuild SelectByParent_id(params int?[] Parent_id) => Select.WhereParent_id(Parent_id);

		#region async
		async public static Task<List<CategoryInfo>> DeleteByParent_idAsync(int Parent_id) {
			var items = await dal.DeleteByParent_idAsync(Parent_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return items;
		}
		async public static Task<CategoryInfo> DeleteAsync(int Id) {
			var item = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<CategoryInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("TEST_BLL:Category:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync());
		public static Task<int> UpdateAsync(CategoryInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(CategoryInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<CategoryInfo> InsertAsync(int? Parent_id, string Name) {
			return InsertAsync(new CategoryInfo {
				Parent_id = Parent_id, 
				Name = Name});
		}
		async public static Task<CategoryInfo> InsertAsync(CategoryInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<CategoryInfo>> InsertAsync(IEnumerable<CategoryInfo> items) {
			foreach (var item in items) if (item != null && item.Create_time == null) item.Create_time = DateTime.Now;
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		internal static Task RemoveCacheAsync(CategoryInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<CategoryInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("TEST_BLL:Category:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<CategoryInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<CategoryInfo>> GetItemsByParent_idAsync(params int?[] Parent_id) => Select.WhereParent_id(Parent_id).ToListAsync();
		public static Task<List<CategoryInfo>> GetItemsByParent_idAsync(int?[] Parent_id, int limit) => Select.WhereParent_id(Parent_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<CategoryInfo, SelectBuild> {
			public SelectBuild WhereParent_id(params int?[] Parent_id) => this.Where1Or(@"a.[parent_id] = {0}", Parent_id);
			public SelectBuild WhereParent_id(Category.SelectBuild select, bool isNotIn = false) => this.Where($@"a.[parent_id] {(isNotIn ? "NOT IN" : "IN")} ({select.ToString(@"[id]")})");
			public SelectBuild WhereId(params int[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
			public SelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
			public SelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
			public SelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where(@"a.[create_time] >= {0}", begin);
			public SelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereCreate_timeRange(begin) : base.Where(@"a.[create_time] between {0} and {1}", begin, end);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or(@"a.[name] = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.[name] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}