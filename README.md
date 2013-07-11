MiniuiExtension
===============

minuiui是一个用于asp.net mvc中的一个HtmlHelper扩展，可以方便的生成miniui代码
使用方法如下

@using MiniUIExtension; 
@model IEnumerable<Models.User>
@{
    ViewBag.Title = "Index";
}
<!--撑满页面-->
<div class="mini-fit">
    @Html.GridViewFor(Model, "gridview1").AutoGenerateColumns().SetUrl(Request.RawUrl).SetMultiSelect(true)
    
    @Html.GridViewFor(Model, "gridview1").SetColumns(c =>
{
    c.Add(a => a.UserName);
    c.Add(a => a.Age).SetSortable(true);
    c.Add(a => a.Phone);
    c.Add(a => a.CreateDate).SetHtmlAttributes(new { dateFormat = "yyyy-MM-dd" });
}).SetUrl(Request.RawUrl).SetMultiSelect(false)

</div>
<script type="text/javascript">
    mini.parse();
    var grid = mini.get("gridview1");
    grid.load();
</script>

