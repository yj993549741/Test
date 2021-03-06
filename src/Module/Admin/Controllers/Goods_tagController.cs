﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TEST.BLL;
using TEST.Model;

namespace TEST.Module.Admin.Controllers {
	[Route("[controller]")]
	public class Goods_tagController : BaseController {
		public Goods_tagController(ILogger<Goods_tagController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] int?[] Tag_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Goods_tag.Select;
			if (Tag_id.Length > 0) select.WhereTag_id(Tag_id);
			var items = await select.Count(out var count)
				.LeftJoin(a => a.Obj_tag.Id == a.Tag_id).Page(page, limit).ToListAsync();
			ViewBag.items = items;
			ViewBag.count = count;
			return View();
		}

		[HttpGet(@"add")]
		public ActionResult Edit() {
			return View();
		}
		[HttpGet(@"edit")]
		async public Task<ActionResult> Edit([FromQuery] int Goods_id) {
			Goods_tagInfo item = await Goods_tag.GetItemAsync(Goods_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Goods_id, [FromForm] int? Tag_id) {
			Goods_tagInfo item = new Goods_tagInfo();
			item.Goods_id = Goods_id;
			item.Tag_id = Tag_id;
			item = await Goods_tag.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Goods_id, [FromForm] int? Tag_id) {
			Goods_tagInfo item = await Goods_tag.GetItemAsync(Goods_id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Tag_id = Tag_id;
			int affrows = await Goods_tag.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			var dels = new List<object>();
			foreach (int id2 in id)
				dels.Add(await Goods_tag.DeleteAsync(id2));
			if (dels.Count > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{dels.Count}").SetData("dels", dels);
			return APIReturn.失败;
		}
	}
}
