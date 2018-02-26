(function(q) {
    function r(a, b) {
        a = JSON.stringify(b);
        eval("(" + a + ")");
        var c = b.geometry.coordinates;
        a = [];
        for (var d = 0; d < c.length; d++)
            a.push([c[d][1], c[d][0]]);
        var c = b.name
          , e = b.color;
        if ("" != e)
            var d = parseInt(parseInt(e) / 65536)
              , g = parseInt(parseInt(e) % 65536 / 256)
              , f = parseInt(e) - 65536 * d - 256 * g
              , e = "#" + (16777216 + (d << 16) + (g << 8) + f).toString(16).slice(1);
        var d = b.metieria
          , g = b.drawDate
          , f = b.diameter
          , n = b.length;
        if ("" == e || "0" == e)
            e = -1 != c.indexOf("\u6ce8\u6c34") ? "#0000EE" : -1 != c.indexOf("\u5929\u7136\u6c14") ? "#fafc0d" : -1 != c.indexOf("\u6df7\u8f93") ? "red" : -1 != c.indexOf("\u539f\u6cb9") ? "#000000" : "#FFFFFF";
        var k = {
            color: e,
            weight: 4,
            opacity: 1
        }
          , t = {
            color: e,
            weight: 4,
            opacity: 0
        }
          , m = L.geoJson(b, {
            style: k
        }).addTo(h.pipelines);
        m.bindPopup(c + "</br>\u7ba1\u7ebf\u957f\u5ea6\uff1a" + Number(n).toFixed(2) + "\u5343\u7c73</br>\u5185\u7ba1\u76f4\u5f84\uff1a" + f + "\u82f1\u5bf8</br>\u6295\u4ea7\u5e74\u4efd\uff1a" + g + "\u5e74</br>\u8f93\u9001\u4ecb\u8d28\uff1a" + d + "</br>\u957f\u671f\u822a\u544a\u7f16\u53f7\uff1a");
        m.bindTooltip(c, {
            direction: "center",
            sticky: !0
        });
        var l = L.polylineDecorator(a, {
            patterns: [{
                offset: 75,
                repeat: "40%",
                symbol: L.Symbol.arrowHead({
                    pixelSize: 15,
                    pathOptions: {
                        color: e,
                        fillOpacity: .7,
                        weight: 0
                    }
                })
            }]
        }).addTo(h.pipelines);
        map.on("zoomend", function() {
            5 > map._zoom ? (l.setPatterns([{
                offset: 75,
                repeat: "40%",
                symbol: L.Symbol.arrowHead({
                    pixelSize: 15,
                    pathOptions: {
                        color: e,
                        fillOpacity: 0,
                        weight: 0
                    }
                })
            }]),
            m.setStyle(t)) : (l.setPatterns([{
                offset: 75,
                repeat: "40%",
                symbol: L.Symbol.arrowHead({
                    pixelSize: 15,
                    pathOptions: {
                        color: e,
                        fillOpacity: .7,
                        weight: 0
                    }
                })
            }]),
            m.setStyle(k))
        })
    }
    function u(a, b) {
        v(b);
        map.on("zoomend", function() {
            5 > map._zoom ? $.each(k, function(a, b) {
                b.setOpacity(0)
            }) : $.each(k, function(a, b) {
                b.setOpacity(1)
            });
            5 > map._zoom ? $.each(l, function(a, b) {
                b.setOpacity(0)
            }) : $.each(l, function(a, b) {
                b.setOpacity(1)
            })
        })
    }
    function v(a) {
        var b = $.trim(a.id)
          , c = a.geometry.coordinates
          , c = [c[1], c[0]];
        a = a.geometry.ename;
        var d = c
          , e = '<div id="div_1_' + b + '">' + ('<div class="homeScreen" ><div class="mask" ><div class="allScreens" id="allScreens-' + b + '"></div></div>\t<ul class="indicators"  id="indicators-' + b + '"></ul></div>') + "</div>"
          , g = L.icon({
            iconUrl: "../Images/MapIcon/PT-1.png",
            iconSize: [40, 40]
        })
          , d = L.marker(d, {
            icon: g,
            title: a,
            riseOnHover: !0
        });
        d.bindPopup(e);
        d.addTo(h.platforms);
        d.markerID = b;
        d.markerURL = "../Images/MapIcon/PT-1.png";
        d.markerTip = a;
        d.markerWidth = 40;
        d.markerHeight = 40;
        d.ProductionUnitType = "\u5e73\u53f0";
        d.on("popupopen", function(a) {
            a = a.target;
            try {
                messageSignal.sendMessage("PINGTAICHANGE", a.markerID)
            } catch (n) {}
            w(a.markerID);
            map && map.panTo(c)
        });
        l.push(d);
        e = L.divIcon({
            iconAnchor: [-26, 0],
            className: "icon-marker-location-label",
            html: "<p  class='map-label'>" + a + "</p>"
        });
        "JZ21-1WHPA" == $.trim(d.markerTip) ? e = L.divIcon({
            iconAnchor: [100, 35],
            className: "icon-marker-location-label",
            html: "<p   class='map-label'>" + a + "</p>"
        }) : -1 != $.trim(d.markerTip).indexOf("CEP") && (e = L.divIcon({
            iconAnchor: [90, 35],
            className: "icon-marker-location-label",
            html: "<p   class='map-label'>" + a + "</p>"
        }));
        a = L.marker(c, {
            icon: e
        }).addTo(h.platforms);
        a.markerID = b;
        a.markerFacility = d;
        a.on("click", function(a) {
            a = a.target;
            try {
                a && a.markerFacility && a.markerFacility.openPopup()
            } catch (n) {}
        });
        k.push(a)
    }
    function w(a) {
        var b, c, d;
        b = new Icon("CCTV" + a,"\u89c6\u9891\u52a8\u6001","../images/icon/CCTV_i4.png","");
        c = new Icon("SCRB" + a,"\u5de5\u4f5c\u65e5\u62a5","../images/icon/SCRB.png","");
        var e = new Icon("PTXX" + a,"\u5e73\u53f0/\u7ec8\u7aef\u4fe1\u606f","../images/icon/PTXX_i2.png","")
          , g = new Icon("RYXX" + a,"\u4eba\u5458\u4fe1\u606f","../images/icon/gongzuoribao.png","")
          , f = new Icon("YJGL" + a,"\u5e94\u6025\u7ba1\u7406","../images/icon/SBBJ_i.png","")
          , h = new Icon("YSKC" + a,"\u6cb9\u6c34\u5e93\u5b58","../images/icon/WXGD_i.png","");
        b = [b, c, f, g, h, e];
        c = $("#allScreens-" + a);
        c.Touchable();
        d = new Stage(b);
        d.addScreensTo(c);
        d.addIndicatorsTo("#indicators-" + a);
        c.bind("touchablemove", function(a, b) {
            -5 > b.currentDelta.x && d.next();
            if (5 < b.currentDelta.x)
                return d.previous()
        })
    }
    function x(a) {
        a = L.control({
            position: "bottomright"
        });
        a.onAdd = function(a) {
            this._div = L.DomUtil.create("div", "");
            this.update();
            return this._div
        }
        ;
        a.update = function(a) {
            a && (this._div.innerHTML = '<div style="padding:10px;opacity:0.8;background:#BAC2D8;border-radius:5px;color:#12385F;font-size:12px;font-weight:bolder;">(' + p(a.latlng.lat) + "," + p(a.latlng.lng) + ")</div>")
        }
        ;
        a.addTo(map);
        return a
    }
    function p(a) {
        var b = Math.floor(a)
          , c = 60 * (a - b);
        a = Math.floor(c);
        c = Math.round(60 * (c - a));
        60 == c && (a++,
        c = 0);
        60 == a && (b++,
        a = 0);
        return "" + b + "\u00b0" + a + "'" + c + '"'
    }
    var h = {
        pipelines: new L.LayerGroup,
        platforms: new L.LayerGroup,
        polygons: new L.LayerGroup
    }
      , l = []
      , k = []
      , y = {
        "\u52d8\u63a2\u534f\u540c": {
            "\u7ba1\u7ebf": h.pipelines,
            "\u5e73\u53f0": h.platforms,
            "\u9762\u6570\u636e": h.polygons
        }
    };
    q.cnoocGis = {
        initialMap: function() {
            map = L.map("mapContent", {
                center: [20.0508, 119.2066],
                zoom: 6,
                zoomControl: !1,
                crs: L.CRS.EPSG4326,
                measureControl: !0,
                layers: [h.pipelines, h.platforms, h.polygons],
                maxBounds: [[8.4375, 91], [43.59375, 145]]
            });
            var a = {
                "\u5f71\u50cf\u5e95\u56fe1��": L.tileLayer("http://10.78.165.180:8080/geoserver/gwc/service/tms/1.0.0/cite%3Achina@EPSG%3A4326@jpeg/{z}/{x}/{y}.jpg", {
                    minZoom: 5,
                    maxZoom: 13,
                    attribution: "<div id='tdiv' style=\"padding-right:10px;\"></div> &copy <a>\u603b\u516c\u53f8\u5730\u7406\u4fe1\u606f\u7cfb\u7edf\u5e73\u53f0v1.0</a>&nbsp&nbsp&nbsp",
                    tms: !0
                }),
                "\u7535\u5b50\u6d77\u56fe": L.tileLayer("http://10.78.165.180:8081/newtile/Tiles/Z{z}/{x}/{y}/{x}_{y}.png", {
                    minZoom: 7,
                    maxZoom: 14,
                    attribution: "<div id='tdiv' style=\"padding-right:10px;\"></div> &copy <a>\u603b\u516c\u53f8\u5730\u7406\u4fe1\u606f\u7cfb\u7edf\u5e73\u53f0v1.0</a>&nbsp&nbsp&nbsp"
                })
            }
              , b = {
                "\u5f71\u50cf\u5e95\u56fe": L.tileLayer("http://10.78.165.180:8080/geoserver/gwc/service/tms/1.0.0/cite%3Achina@EPSG%3A4326@jpeg/{z}/{x}/{y}.jpg", {
                    minZoom: 3,
                    attribution: "<div id='tdiv' style=\"padding-right:10px;\"></div> &copy <a>\u603b\u516c\u53f8\u5730\u7406\u4fe1\u606f\u7cfb\u7edf\u5e73\u53f0v1.0</a>&nbsp&nbsp&nbsp",
                    tms: !0
                }),
                "\u7535\u5b50\u6d77\u56fe": L.tileLayer("http://10.78.165.180:8081/newtile/Tiles/Z{z}/{x}/{y}/{x}_{y}.png", {
                    minZoom: 3,
                    attribution: "<div id='tdiv' style=\"padding-right:10px;\"></div> &copy <a>\u603b\u516c\u53f8\u5730\u7406\u4fe1\u606f\u7cfb\u7edf\u5e73\u53f0v1.0</a>&nbsp&nbsp&nbsp"
                })
            };
            //map.addLayer(a.\u5f71\u50cf\u5e95\u56fe1��);
            map.addLayer(a["\u5f71\u50cf\u5e95\u56fe1��"]);
            L.control.groupedLayers(a, y).addTo(map);
            var c = (new L.Control.MiniMap(b.\u5f71\u50cf\u5e95\u56fe,{
                zoomLevelFixed: 5,
                toggleDisplay: !0
            })).addTo(map);
            L.Map.prototype.setCrs = function(a) {
                this.options.crs = a
            }
            ;
            map.on("baselayerchange", function(a) {
                var d = map.getCenter();
                "\u5f71\u50cf\u5e95\u56fe" == a.name ? (map.setCrs(L.CRS.EPSG4326),
                c._layer._map.options.crs = L.CRS.EPSG4326,
                map.setMaxBounds([[8.4375, 91], [43.59375, 145]]),
                map.setView(d, "6")) : (map.setCrs(L.CRS.EPSG3857),
                map.setView(d, "7"),
                c._layer._map.options.crs = L.CRS.EPSG3857,
                map.setMaxBounds([[3, 106.907], [41.34805555, 133]]));
                c.changeLayer(b[a.name])
            });
            L.control.scale().addTo(map);
            map.addControl(new L.Control.Position);
            L.Control.zoomHome({
                position: "topright"
            }).addTo(map);
            a = {
                actions: {
                    alert: {
                        display: "\u8fd4\u56de\u6240\u9009\u56fe\u5c42ID",
                        action: L.Control.BoxSelector.Actions.alert()
                    }
                }
            };
            (new L.Control.BoxSelector(a)).addTo(map);
            var d = x();
            map.on("mousemove", function(a) {
                d.update(a)
            })
        },
        getLineData: function() {
            $.post("home/getpipeline", function(a) {
                $.each(a, function(a, c) {
                    r(a, c)
                })
            })
        },
        getPointData: function() {
            $.post("home/getPlatForm", function(a) {
                $.each(a, function(a, c) {
                    u(a, c)
                })
            })
        },
        getPolygonData: function() {
            $.post("home/getPolygon", function(a) {
                $.each(a, function(a, c) {
                    JSON.stringify(c);
                    a = c.id.split(",");
                    $.trim(a[0]);
                    var b = a[1];
                    a = a[2];
                    var e = parseInt(parseInt(b) / 65536)
                      , g = parseInt(parseInt(b) % 65536 / 256)
                      , b = parseInt(b) - 65536 * e - 256 * g
                      , b = {
                        color: "#" + (16777216 + (e << 16) + (g << 8) + b).toString(16).slice(1),
                        opacity: 1
                    };
                    L.geoJson(c, {
                        style: b
                    }).addTo(h.polygons).bindTooltip(a, {
                        direction: "center"
                    }).addTo(map)
                })
            })
        },
        Legend: function() {
            var a = L.control({
                position: "bottomleft"
            });
            a.onAdd = function(a) {
                this._div = L.DomUtil.create("div", "");
                this.update();
                return this._div
            }
            ;
            a.update = function(a) {
                this._div.innerHTML = '<div style="padding:10px;background:rgba(255, 255, 255, 0.2) !important;filter: alpha(opacity=20);  background:#BAC2D8;border-radius:5px;color:#12385F;font-size:14px;font-weight:bold;"><span style="color:#fafc0d;">\u2014\u2014\u2014\u2014\u5929\u7136\u6c14</span></br><span  style="color:red;">\u2014\u2014\u2014\u2014\u6df7\u8f93</span></br><span  style="color:#000000;">\u2014\u2014\u2014\u2014\u539f\u6cb9</span></br><span  style="color:#0000EE;">\u2014\u2014\u2014\u2014\u6ce8\u6c34</span></div>'
            }
            ;
            a.addTo(map);
            return a
        },
        showtime: function() {
            var a, b, c, d, e, g, f;
            a = new Date;
            switch (a.getDay()) {
            case 0:
                f = "\u661f\u671f\u65e5";
                break;
            case 1:
                f = "\u661f\u671f\u4e00";
                break;
            case 2:
                f = "\u661f\u671f\u4e8c";
                break;
            case 3:
                f = "\u661f\u671f\u4e09";
                break;
            case 4:
                f = "\u661f\u671f\u56db";
                break;
            case 5:
                f = "\u661f\u671f\u4e94";
                break;
            case 6:
                f = "\u661f\u671f\u516d";
                break;
            case 7:
                f = "\u661f\u671f\u65e5"
            }
            d = a.getFullYear();
            e = a.getMonth() + 1;
            g = a.getDate();
            b = a.getHours();
            c = a.getMinutes();
            a = a.getSeconds();
            $("#tdiv")[0].innerHTML = d + "\u5e74" + e + "\u6708" + g + "\u65e5 " + f + " " + b + ":" + c + ":" + a;
            setTimeout("cnoocGis.showtime();", 1E3)
        },
        locationPoint: function() {
            var a = L.marker([39.3530555, 119.641111]);
            a.on("click", function(a) {
                a.target && map.setView([40.1013888, 121.151666], 9)
            });
            var b = !1;
            map.on("zoomend", function() {
                6 == map._zoom ? 0 == b && (a.addTo(map),
                b = !0) : 6 < map._zoom && (a.remove(),
                b = !1)
            })
        }
    }
})(window);