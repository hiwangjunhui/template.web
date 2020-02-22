//取窗口可视范围的高度
function getClientHeight() {
	var clientHeight = 0;
	if (document.body.clientHeight && document.documentElement.clientHeight) {
		var clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight; //如果a<b，则执行前者，否则执行后者
	}
	else {
		var clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
	}
	return clientHeight;
}
//滚动条距离顶部的高度
function getScrollTop() {
	var scrollTop = 0;
	if (document.documentElement && document.documentElement.scrollTop) {
		scrollTop = document.documentElement.scrollTop;
	}
	else if (document.body) {
		scrollTop = document.body.scrollTop;
	}
	return scrollTop;
}
//取文档内容实际高度
function getScrollHeight() {
	return Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
}


window.onscroll = function () {
	var height = getClientHeight(); //取窗口可视范围的高度
	var theight = getScrollTop(); //滚动条距离顶部的高度
	var rheight = getScrollHeight(); //取文档内容实际高度
	var bheight = rheight - theight - height;

	if (theight > 780 && theight < 780 + document.getElementById("block_info").offsetHeight - document.getElementById("anchor_jbx").offsetHeight) {
		document.getElementById("anchor_jbx").className = "anchor_jbx";
		//document.getElementById("anchor_jbx").style.display = "block";
	}
	else {
		document.getElementById("anchor_jbx").className = "";
		//document.getElementById("anchor_jbx").style.display = "none";
	}
}