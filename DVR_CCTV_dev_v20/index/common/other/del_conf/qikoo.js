var qikoo = window.qikoo || {};
qikoo.dialog = function() {
    var e, t, n = {}, r = function() {
            var n = ['<div class="mod-dialog-bg"></div>', '<div class="mod-dialog">', '<div class="dialog-nav">', '<span class="dialog-title"></span>', '<a href="#" onclick="return false" class="dialog-close"></a>', "</div>", '<div class="dialog-main"></div>', "</div>"].join(""),
                r = $(n).hide().appendTo("body");
            e = r.filter(".mod-dialog-bg"), t = r.filter(".mod-dialog"), t.find(".dialog-close").click(function() {
                u()
            })
        }, i = function() {
            t.css("width", n.width || ""), t.find(".dialog-title").html(n.title), t.find(".dialog-main").html(n.html), t.show(), e.show(), s()
        }, s = function() {
            var e = ($(window).width() - t.width()) / 2,
                n = ($(window).height() - t.height()) / 2;
            n = n > 0 ? n + $(window).scrollTop() : 0, t.css({
                left: e,
                top: n
            })
        }, o = function(e) {
            return typeof e != "object" && (e = {
                html: e || ""
            }), n = $.extend({
                title: "",
                html: "",
                closeFn: null
            }, e), t || r(), i(), t
        }, u = function() {
            e && e.hide(), t && t.hide(), n.closeFn && n.closeFn.call(this)
        };
    return {
        show: o,
        hide: u
    }
}(), qikoo.dialog.confirm = function(e, t, n) {
    var r = ['<div class="dialog-content">', "<p>" + e + "</p>", "</div>", '<div class="dialog-console clearfix_new">', '<a class="console-btn-cancel" href="#" onclick="return false;">取消</a>','<a class="console-btn-confirm" href="#" onclick="del();">确定</a>', "</div>"].join(""),
        i = qikoo.dialog.show({
            html: r
        });
    return i.find(".console-btn-confirm").click(function() {
        var e = t && t.call(i);
        e !== !1 && qikoo.dialog.hide()
    }), i.find(".console-btn-cancel").click(function() {
        n && n.call(i), qikoo.dialog.hide()
    }), i
}, qikoo.dialog.alert = function(e, t) {
    var n = ['<div class="dialog-content">', "<p>" + e + "</p>", "</div>", '<div class="dialog-console clearfix_new">', '<a class="console-btn-confirm" href="#" onclick="return false;">确定</a>', "</div>"].join(""),
        r = qikoo.dialog.show({
            html: n
        });
    return r.find(".console-btn-confirm").click(function() {
        var e = t && t.call(r);
        e !== !1 && qikoo.dialog.hide()
    }), r
}, qikoo.dialog.payNotice = function(e, t, n) {
    e = e || function() {
        window.location.reload()
    }, t = t || function() {
        window.location.reload()
    };
    var r = qikoo.dialog.show({
        html: [''].join(""),
        width: 530,
        closeFn: n
    });
    r.find(".console-btn-confirm").click(function() {
        e(), qikoo.dialog.hide()
    }), r.find(".console-btn-cancel").click(function() {
        t(), qikoo.dialog.hide()
    })
}, qikoo.pop = function(e) {
    var t, n = ['<div class="mod-pop">', '<div class="pop-nav">', '<span class="pop-title"></span>', '<a class="pop-close" href="#" onclick="return false"></a>', "</div>", '<div class="pop-main"></div>', "</div>"].join("");
    return t = $(n).hide().appendTo("body"), t.find(".pop-close").click(function() {
        t.remove()
    }), e = $.extend({
        title: "",
        html: ""
    }, e), t.find(".pop-title").html(e.title), t.find(".pop-main").html(e.html), t
}, qikoo.popConfirm = function() {
    var e, t = function(e) {
            var t = ["<p>" + e + "</p>", '<div class="pop-console">', '<a class="pop-btn-green" href="#" onclick="return false">确定</a>', '<a class="pop-btn-gray" href="#" onclick="return false">取消</a>', "</div>"].join("");
            return qikoo.pop({
                title: "",
                html: t
            })
        }, n = function(t) {
            var n = $(t).offset();
            n.left = n.left + $(t).width() - e.width(), n.top = n.top + $(t).height(), n.left < 0 && (n.left = 0), n.top < 0 && (n.top = 0), e.css(n)
        }, r = function(t) {
            e.find(".pop-btn-gray").click(function() {
                i()
            }), e.find(".pop-btn-green").click(function() {
                t && t.call(this), i()
            })
        }, i = function() {
            e && e.remove(), e = null
        };
    return function(s, o, u) {
        i(), e = t(o), n(s), r(u), e.show()
    }
	
	function del(){
		$(".console-btn-confirm").click(function(){
			$("tr").hide();
		})
	}
}();