(function(g){var window=this;var Kva=function(a,b){var c=[];g.El(b,function(a){try{var b=g.nD.prototype.o.call(this,a,!0)}catch(f){if("Storage: Invalid value was encountered"==f)return;throw f;}g.t(b)?g.mD(b)&&c.push(a):c.push(a)},a);
return c},Lva=function(a,b){var c=Kva(a,b);
(0,g.H)(c,function(a){g.nD.prototype.remove.call(this,a)},a)},Y8=function(a,b){g.W0.call(this,g.T("YTP_MDX_TITLE"),0,a,b);
this.U=a;this.F={};this.T(a,"onMdxReceiversChange",this.J);this.T(a,"presentingplayerstatechange",this.J);this.J()},Mva=function(){var a=g.LG;
Lva(a,a.g.Sd(!0))},Z8=function(a){g.nH.call(this,"ScreenServiceProxy");
this.bd=a;this.o=[];this.o.push(this.bd.$_s("screenChange",(0,g.A)(this.lS,this)));this.o.push(this.bd.$_s("onlineScreenChange",(0,g.A)(this.zO,this)))},$8=function(a){g.aH("cloudview",a)},Nva=function(a){$8("setApiReady_ "+a);
g.v("yt.mdx.remote.cloudview.apiReady_",a,void 0)},a9=function(){return g.x("yt.mdx.remote.cloudview.instance_")},Ova=function(a){g.cG[a]&&(a=g.cG[a],(0,g.H)(a,function(a){g.aG[a]&&delete g.aG[a]}),a.length=0)},b9=function(){return g.x("yt.mdx.remote.connection_")},c9=function(a){g.v("yt.mdx.remote.connectData_",a,void 0)},Pva=function(a){g.v("yt.mdx.remote.currentScreenId_",a,void 0)},d9=function(){return g.x("yt.mdx.remote.currentScreenId_")},f9=function(){if(!e9){var a=g.x("yt.mdx.remote.screenService_");
e9=a?new Z8(a):null}return e9},g9=function(a){g.v("yt.mdx.remote.cloudview.initializing_",a,void 0)},h9=function(){return!!g.x("yt.mdx.remote.cloudview.apiReady_")},i9=function(a){$8("setCastInstalled_ "+a);
g.NG("yt-remote-cast-installed",a)},j9=function(a){g.aH("cloudview",a)},Qva=function(a,b){a9().init(a,b)},k9=function(){return!!g.OG("yt-remote-cast-installed")},Rva=function(){$8("dispose");
var a=a9();a&&a.dispose();g.v("yt.mdx.remote.cloudview.instance_",null,void 0);Nva(!1);g.eG(l9);l9.length=0},Sva=function(){var a=window.document.createElement("a");
g.Nd(a,"https://www.gstatic.com/cv/js/sender/v1/cast_sender.js");a=a.href.replace(/^[a-zA-Z]+:\/\//,"//");return"js-"+g.Ta(a)},Tva=function(a,b){var c=window.document.createElement("script");
c.id=a;c.onload=function(){b&&(0,window.setTimeout)(b,0)};
c.onreadystatechange=function(){switch(c.readyState){case "loaded":case "complete":c.onload()}};
c.src="https://www.gstatic.com/cv/js/sender/v1/cast_sender.js";var d=window.document.getElementsByTagName("head")[0]||window.document.body;d.insertBefore(c,d.firstChild);return c},Uva=function(a){var b=g.$F();
if(b)if(b.clear(a),a)Ova(a);else for(var c in g.cG)Ova(c)},m9=function(){return g.x("yt.mdx.remote.channelParams_")||{}},o9=function(a){var b=b9();
c9(null);a||Pva("");g.v("yt.mdx.remote.connection_",a,void 0);n9&&((0,g.H)(n9,function(b){b(a)}),n9.length=0);
b&&!a?g.hG("yt-remote-connection-change",!1):!b&&a&&g.hG("yt-remote-connection-change",!0)},Vva=function(){return g.x("yt.mdx.remote.connectData_")},p9=function(){var a=d9();
if(!a)return null;var b=f9().Yd();return g.mH(b,a)},Wva=function(a,b){i9(!0);
g9(!1);Qva(a,function(a){a?(Nva(!0),g.fG("yt-remote-cast2-api-ready")):(j9("Failed to initialize cast API."),i9(!1),g.PG("yt-remote-cast-available"),g.PG("yt-remote-cast-receiver"),Rva());b(a)})},Xva=function(){return k9()?a9()?a9().getCastSession():(j9("getCastSelector: Cast is not initialized."),null):(j9("getCastSelector: Cast API is not installed!"),null)},Yva=function(){var a=Sva(),b=window.document.getElementById(a),c=b&&g.qF(b,"loaded");
c||b&&!c||(b=Tva(a,function(){g.qF(b,"loaded")||(g.rF(b,"loaded","true"),g.fG(a),g.xE(g.ya(Uva,a),0))}))},Zva=function(a){return(0,g.I)(a,function(a){return{key:a.id,
name:a.name}})},$va=function(){if(g.Moa){var a=2,b=g.Dh(),c=function(){a--;
0==a&&b&&b(!0)};
window.__onGCastApiAvailable=c;g.Ih("//www.gstatic.com/cast/sdk/libs/sender/1.0/cast_framework.js",g.Fh,c)}},r9=function(a,b){d9();
p9()&&p9();Pva(a.id);var c=new g.rI(q9,a,m9());c.connect(b,Vva());c.subscribe("beforeDisconnect",function(a){g.hG("yt-remote-before-disconnect",a)});
c.subscribe("beforeDispose",function(){b9()&&(b9(),o9(null))});
o9(c)},s9=function(){var a=g.sH();
if(!a)return null;var b=f9().Yd();return g.mH(b,a)},t9=function(a){g.aH("remote",a)},u9=function(){var a=b9();
return!!a&&3!=a.getProxyState()},v9=function(){h9()?a9().stopSession():j9("stopSession called before API ready.");
var a=b9();a&&(a.disconnect(1),o9(null))},awa=function(){var a=f9().bd.$_gos();
var b=p9();b&&b9()&&(g.lH(a,b)||a.push(b));return Zva(a)},x9=function(a,b){g.RF.call(this);
this.g=0;this.B=a;this.D=[];this.C=new g.BB;this.A=this.o=null;this.J=(0,g.A)(this.GM,this);this.F=(0,g.A)(this.Sk,this);this.G=(0,g.A)(this.FM,this);this.K=(0,g.A)(this.TM,this);var c=0;a?(c=a.getProxyState(),3!=c&&(a.subscribe("proxyStateChange",this.kt,this),bwa(this))):c=3;0!=c&&(b?this.kt(c):g.xE((0,g.A)(function(){this.kt(c)},this),0));
var d=Xva();d&&w9(this,d);this.subscribe("yt-remote-cast2-session-change",this.K)},y9=function(a){return new g.gI(a.B.getPlayerContextData())},cwa=function(a,b,c,d,e){var f=y9(a),k=e||f.listId;
d=d||0;var l={videoId:b,currentIndex:d};g.kI(f,b,d);g.t(c)&&(g.iI(f,c),l.currentTime=c);g.t(k)&&(l.listId=k);z9(a,"setPlaylist",l);e||A9(a,f)},bwa=function(a){(0,g.H)("nowAutoplaying autoplayDismissed remotePlayerChange remoteQueueChange autoplayModeChange autoplayUpNext previousNextChange".split(" "),function(a){this.D.push(this.B.subscribe(a,g.ya(this.RO,a),this))},a)},dwa=function(a){(0,g.H)(a.D,function(a){this.B.unsubscribeByKey(a)},a);
a.D.length=0},B9=function(a,b){50>a.C.Nc()&&g.DB(a.C,b)},C9=function(a,b,c){var d=y9(a);
g.iI(d,c);-1E3!=d.g&&(d.g=b);A9(a,d)},z9=function(a,b,c){a.B.sendMessage(b,c)},A9=function(a,b){dwa(a);
a.B.setPlayerContextData(g.lI(b));bwa(a)},w9=function(a,b){a.A&&(a.A.removeUpdateListener(a.J),a.A.removeMediaListener(a.F),a.Sk(null));
a.A=b;a.A&&(D9("Setting cast session: "+a.A.sessionId),a.A.addUpdateListener(a.J),a.A.addMediaListener(a.F),a.A.media.length&&a.Sk(a.A.media[0]))},ewa=function(a){var b=a.o.media,c=a.o.customData;
if(b&&c){var d=y9(a);b.contentId!=d.videoId&&D9("Cast changing video to: "+b.contentId);d.videoId=b.contentId;d.g=c.playerState;g.iI(d,a.o.getEstimatedTime());A9(a,d)}else D9("No cast media video. Ignoring state update.")},D9=function(a){g.aH("CP",a)},E9=function(a,b,c){return(0,g.A)(function(a){this.qc("Failed to "+b+" with cast v2 channel. Error code: "+a.code);
a.code!=window.chrome.cast.ErrorCode.TIMEOUT&&(this.qc("Retrying "+b+" using MDx browser channel."),z9(this,b,c))},a)},fwa=function(a,b){var c=!1;
a9()||(c=new g.aI(a,b),c.subscribe("yt-remote-cast2-availability-change",function(a){g.NG("yt-remote-cast-available",a);g.hG("yt-remote-cast2-availability-change",a)}),c.subscribe("yt-remote-cast2-receiver-selected",function(a){$8("onReceiverSelected: "+a.friendlyName);
g.NG("yt-remote-cast-receiver",a);g.hG("yt-remote-cast2-receiver-selected",a)}),c.subscribe("yt-remote-cast2-receiver-resumed",function(a){$8("onReceiverResumed: "+a.friendlyName);
g.NG("yt-remote-cast-receiver",a)}),c.subscribe("yt-remote-cast2-session-change",function(a){$8("onSessionChange: "+g.iH(a));
a||g.PG("yt-remote-cast-receiver");g.hG("yt-remote-cast2-session-change",a)}),g.v("yt.mdx.remote.cloudview.instance_",c,void 0),c=!0);
$8("cloudview.createSingleton_: "+c);return c},gwa=function(){var a=0<=g.Gb.search(/\ (CrMo|Chrome|CriOS)\//);
return g.UE||a},F9=function(a,b){h9()?a9().setConnectedScreenStatus(a,b):j9("setConnectedScreenStatus called before ready.")},hwa=function(){$8("clearCurrentReceiver");
g.PG("yt-remote-cast-receiver")},iwa=function(){if(0<=window.navigator.userAgent.indexOf("CriOS")){var a=window.__gCrWeb&&window.__gCrWeb.message&&window.__gCrWeb.message.invokeOnHost;
if(a){$va();a({command:"cast.sender.init"});return}}if(window.chrome)if($va(),a=window.navigator.userAgent,0<=a.indexOf("Android")&&0<=a.indexOf("Chrome/")&&window.navigator.presentation){var a="",b=g.Bh();55<=b?a="55":50<=b&&(a="50");g.Ih("//www.gstatic.com/eureka/clank"+a+g.Gh,g.Fh)}else g.Jh(0);else g.Fh()},jwa=function(a){a?(g.NG("yt-remote-session-app",a.app),g.NG("yt-remote-session-name",a.name)):(g.PG("yt-remote-session-app"),g.PG("yt-remote-session-name"));
g.v("yt.mdx.remote.channelParams_",a,void 0)},kwa=function(){var a=m9();
if(g.Tb(a)){var a=g.rH(),b=g.OG("yt-remote-session-name")||"",c=g.OG("yt-remote-session-app")||"",a={device:"REMOTE_CONTROL",id:a,name:b,app:c,"mdx-version":3};g.v("yt.mdx.remote.channelParams_",a,void 0)}},lwa=function(){var a=s9();
a?(t9("Resume connection to: "+g.iH(a)),r9(a,0)):(g.AH(),hwa(),t9("Skipping connecting because no session screen found."))},mwa=function(a){t9("remote.onCastSessionChange_: "+g.iH(a));
if(a){var b=p9();b&&b.id==a.id?F9(b.id,"YouTube TV"):(b&&v9(),r9(a,1))}else b9()&&v9()},nwa=function(){var a=awa(),b=p9();
b||(b=s9());return g.$a(a,function(a){return b&&g.fH(b,a.key)?!0:!1})},owa=function(){var a=g.OG("yt-remote-cast-receiver");
return a?a.friendlyName:null},pwa=function(a,b,c,d,e){gwa()?fwa(b,e)&&(g9(!0),window.chrome&&window.chrome.cast&&window.chrome.cast.isAvailable?Wva(a,c):(window.__onGCastApiAvailable=function(b,d){b?Wva(a,c):(j9("Failed to load cast API: "+d),i9(!1),g9(!1),g.PG("yt-remote-cast-available"),g.PG("yt-remote-cast-receiver"),Rva(),c(!1))},d?window.spf?window.spf.script.load("https://www.gstatic.com/cv/js/sender/v1/cast_sender.js","",void 0):Yva():iwa())):$8("Cannot initialize because not running Chrome")},
qwa=function(a){this.port=this.domain="";
this.g="/api/lounge";this.o=!0;a=a||window.document.location.href;var b=g.vg(a)||"";b&&(this.port=":"+b);this.domain=g.ug(a)||"";a=g.Gb;0<=a.search("MSIE")&&(a=a.match(/MSIE ([\d.]+)/)[1],0>g.Sa(a,"10.0")&&(this.o=!1))},rwa=function(a){for(var b,c=[];!(b=a.next()).done;)c.push(b.value);
return c},G9=function(){var a=nwa();
!a&&k9()&&owa()&&(a={key:"cast-selector-receiver",name:owa()});return a},swa=function(){var a=awa();
k9()&&g.OG("yt-remote-cast-available")&&a.push({key:"cast-selector-receiver",name:"Cast..."});return a},twa=function(a){var b={device:"Desktop",
app:"youtube-desktop"};g.LG&&Mva();g.uH();q9||(q9=new qwa,g.CH()&&(q9.g="/api/loungedev"));n9||(n9=g.x("yt.mdx.remote.deferredProxies_")||[],g.v("yt.mdx.remote.deferredProxies_",n9,void 0));kwa();var c=f9();if(!c){var d=new g.NH(q9);g.v("yt.mdx.remote.screenService_",d,void 0);c=f9();pwa(a,d,function(a){a?d9()&&F9(d9(),"YouTube TV"):d.subscribe("onlineScreenChange",function(){g.hG("yt-remote-receiver-availability-change")})},!(!b||!b.loadCastApiSetupScript),b?b.appId:void 0)}b&&!g.x("yt.mdx.remote.initialized_")&&
(g.v("yt.mdx.remote.initialized_",!0,void 0),t9("Initializing: "+g.Lc(b)),H9.push(g.dG("yt-remote-cast2-availability-change",function(){g.hG("yt-remote-receiver-availability-change")})),H9.push(g.dG("yt-remote-cast2-receiver-selected",function(){c9(null);
g.hG("yt-remote-auto-connect","cast-selector-receiver")})),H9.push(g.dG("yt-remote-cast2-receiver-resumed",function(){g.hG("yt-remote-receiver-resumed","cast-selector-receiver")})),H9.push(g.dG("yt-remote-cast2-session-change",mwa)),H9.push(g.dG("yt-remote-connection-change",function(a){a?F9(d9(),"YouTube TV"):s9()||(F9(null,null),hwa())})),a=m9(),b.isAuto&&(a.id+="#dial"),g.aF("desktop_enable_autoplay")&&(a.capabilities=["atp"]),a.name=b.device,a.app=b.app,t9(" -- with channel params: "+g.Lc(a)),
jwa(a),c.start(),d9()||lwa())},I9=function(){k9()?a9()?h9()?($8("Requesting cast selector."),a9().requestSession()):($8("Wait for cast API to be ready to request the session."),l9.push(g.dG("yt-remote-cast2-api-ready",I9))):j9("requestCastSelector: Cast is not initialized."):j9("requestCastSelector: Cast API is not installed!")},J9=function(a,b,c){g.M.call(this);
this.F=a;this.U=b;this.o=new g.CJ(this);g.N(this,this.o);this.o.T(b,"onCaptionsTrackListChanged",this.SN);this.o.T(b,"captionschanged",this.EM);this.o.T(b,"captionssettingschanged",this.cB);this.o.T(b,"videoplayerreset",this.Cn);this.o.T(b,"mdxautoplaycancel",this.yF);this.O=this.o.T(b,"onVolumeChange",this.Bz);this.D=!1;this.g=c;c.subscribe("proxyStateChange",this.Mz,this);c.subscribe("remotePlayerChange",this.Uk,this);c.subscribe("remoteQueueChange",this.Cn,this);c.subscribe("autoplayUpNext",this.mz,
this);c.subscribe("previousNextChange",this.Jz,this);c.subscribe("nowAutoplaying",this.Cz,this);c.subscribe("autoplayDismissed",this.lz,this);this.suggestion=null;this.G=new g.WJ(64);this.A=new g.qt(this.XA,500,this);g.N(this,this.A);this.B=new g.qt(this.YA,1E3,this);g.N(this,this.B);this.C={};this.K=new g.qt(this.nB,1E3,this);g.N(this,this.K);this.J=new g.dk(this.zK,1E3,this);g.N(this,this.J);this.M=g.z;this.cB();this.Cn();this.Uk()},K9=function(a,b){var c=a.F,d=a.U.ia().lengthSeconds;
c.J=b||0;c.g.Y("progresssync",b,d)},uwa=function(a){K9(a,0);
a.A.stop();L9(a,new g.WJ(64))},N9=function(a,b){if(M9(a)&&!a.D){var c=null;
b&&(c={style:a.U.bi()},g.ac(c,b));a.g.RA(a.U.ia().videoId,c);a.C=y9(a.g).o}},O9=function(a,b){var c=a.U.Mf();
c?cwa(a.g,a.U.ia().videoId,b,c.Wd(),c.listId.toString()):cwa(a.g,a.U.ia().videoId,b);L9(a,new g.WJ(1))},vwa=function(a,b){if(b){var c=a.U.Bc("captions","tracklist",{bx:1});
c&&c.length?(a.U.yd("captions","track",b),a.D=!1):(a.U.Hk("captions"),a.D=!0)}else a.U.yd("captions","track",{})},M9=function(a){return y9(a.g).videoId==a.U.ia().videoId},L9=function(a,b){a.B.stop();
var c=a.G;if(!g.bK(c,b)){var d=g.V(b,2);if(d!=g.V(a.G,2)){var e=a.U;g.g2(e.app,d,e.playerType)}a.G=b;wwa(a.F,c,b)}},P9=function(a){g.W.call(this,{H:"div",
N:"ytp-remote",L:[{H:"div",N:"ytp-remote-display-status",L:[{H:"div",N:"ytp-remote-display-status-icon",L:[g.SD()]},{H:"div",N:"ytp-remote-display-status-text",L:["{{statustext}}"]}]}]});this.o=new g.LV(this,250);g.N(this,this.o);this.A=a;this.T(a,"presentingplayerstatechange",this.B);xwa(this,g.VU(a))},xwa=function(a,b){if(3==a.A.Wa()){var c={RECEIVER_NAME:a.A.Bc("remote","currentReceiver").name},c=g.V(b,128)?g.T("YTP_MDX_STATUS_ERROR_2",c):b.wb()||g.V(b,4)?g.T("YTP_MDX_STATUS_PLAYING_2",c):g.T("YTP_MDX_STATUS_CONNECTED_2",
c);
a.Ca("statustext",c);a.o.show()}else a.o.hide()},Q9=function(a){g.AV.call(this,a);
this.A={key:g.kH(),name:g.T("YTP_MDX_MY_COMPUTER")};this.C=null;this.D=[];this.K=this.o=null;this.G=[this.A];this.B=this.A;this.F=new g.WJ(64);this.J=0;var b=g.OU(a).D;b&&(b=b.A&&b.A.g)&&(b=new Y8(a,b),g.N(this,b));b=new P9(a);g.N(this,b);g.pV(a,b.element,5)},wwa=function(a,b,c){a.F=c;
a.g.Y("presentingplayerstatechange",new g.gK(c,b))},R9=function(a,b){if(b.key!=a.B.key)if(b.key==a.A.key)v9();
else{a.B=b;var c=a.g.getPlaylistId();var d=a.g.ia().videoId;if(c||d){var e=a.g.Mf();if(e){var f=[];for(var k=0;k<e.getLength();k++)f[k]=e.jc(k).videoId}else f=[a.g.ia().videoId];c={videoIds:f,listId:c,videoId:d,index:Math.max(a.g.Px(),0),currentTime:a.g.getCurrentTime()}}else c=null;t9("Connecting to: "+g.Lc(b));"cast-selector-receiver"==b.key?(c9(c||null),c=c||null,h9()?a9().setLaunchParams(c):j9("setLaunchParams called before ready.")):!c&&u9()&&d9()==b.key?g.hG("yt-remote-connection-change",!0):
(v9(),c9(c||null),c=f9().Yd(),(c=g.mH(c,b.key))&&r9(c,1))}};
g.p(Y8,g.W0);Y8.prototype.J=function(){var a=this.U.Bc("remote","receivers");a&&1<a.length&&!this.U.Bc("remote","quickCast")?(this.F=g.Cb(a,this.B,this),g.Y0(this,(0,g.I)(a,this.B)),a=this.U.Bc("remote","currentReceiver"),g.X0(this,this.B(a)),this.enable(!0)):this.enable(!1)};
Y8.prototype.B=function(a){return a.key};
Y8.prototype.df=function(a){return"cast-selector-receiver"==a?g.T("YTP_MDX_CAST_SELECTOR"):this.F[a].name};
Y8.prototype.nd=function(a){g.W0.prototype.nd.call(this,a);this.U.yd("remote","currentReceiver",this.F[a]);this.A.Tb()};
g.C(Z8,g.nH);g.h=Z8.prototype;g.h.Yd=function(a){return this.bd.$_gs(a)};
g.h.contains=function(a){return!!this.bd.$_c(a)};
g.h.get=function(a){return this.bd.$_g(a)};
g.h.start=function(){this.bd.$_st()};
g.h.wo=function(a,b,c){this.bd.$_a(a,b,c)};
g.h.remove=function(a,b,c){this.bd.$_r(a,b,c)};
g.h.ho=function(a,b,c,d){this.bd.$_un(a,b,c,d)};
g.h.V=function(){for(var a=0,b=this.o.length;a<b;++a)this.bd.$_ubk(this.o[a]);this.o.length=0;this.bd=null;Z8.aa.V.call(this)};
g.h.lS=function(){this.Y("screenChange")};
g.h.zO=function(){this.Y("onlineScreenChange")};
var e9=null,l9=[],n9=null,q9=null;g.C(x9,g.RF);g.h=x9.prototype;g.h.play=function(){1==this.g?(this.o?this.o.play(null,g.z,E9(this,"play")):z9(this,"play"),C9(this,1,g.jI(y9(this))),this.Y("remotePlayerChange")):B9(this,this.play)};
g.h.pause=function(){1==this.g?(this.o?this.o.pause(null,g.z,E9(this,"pause")):z9(this,"pause"),C9(this,2,g.jI(y9(this))),this.Y("remotePlayerChange")):B9(this,this.pause)};
g.h.xB=function(a){if(1==this.g){if(this.o){var b=y9(this),c=new window.chrome.cast.media.SeekRequest;c.currentTime=a;c.resumeState=b.wb()||3==b.g?window.chrome.cast.media.ResumeState.PLAYBACK_START:window.chrome.cast.media.ResumeState.PLAYBACK_PAUSE;this.o.seek(c,g.z,E9(this,"seekTo",{newTime:a}))}else z9(this,"seekTo",{newTime:a});C9(this,3,a);this.Y("remotePlayerChange")}else B9(this,g.ya(this.xB,a))};
g.h.stop=function(){if(1==this.g){this.o?this.o.stop(null,g.z,E9(this,"stopVideo")):z9(this,"stopVideo");var a=y9(this);a.index=-1;a.videoId="";g.hI(a);A9(this,a);this.Y("remotePlayerChange")}else B9(this,this.stop)};
g.h.setVolume=function(a,b){if(1==this.g){var c=y9(this);if(this.A){if(c.volume!=a){var d=Math.round(a)/100;this.A.setReceiverVolumeLevel(d,(0,g.A)(function(){D9("set receiver volume: "+d)},this),(0,g.A)(function(){this.qc("failed to set receiver volume.")},this))}c.muted!=b&&this.A.setReceiverMuted(b,(0,g.A)(function(){D9("set receiver muted: "+b)},this),(0,g.A)(function(){this.qc("failed to set receiver muted.")},this))}else{var e={volume:a,
muted:b};-1!=c.volume&&(e.delta=a-c.volume);z9(this,"setVolume",e)}c.muted=b;c.volume=a;A9(this,c)}else B9(this,g.ya(this.setVolume,a,b))};
g.h.RA=function(a,b){if(1==this.g){var c=y9(this),d={videoId:a};b&&(c.o={trackName:b.name,languageCode:b.languageCode,sourceLanguageCode:b.translationLanguage?b.translationLanguage.languageCode:"",languageName:b.languageName,format:b.format,kind:b.kind},d.style=g.Lc(b.style),g.ac(d,c.o));z9(this,"setSubtitlesTrack",d);A9(this,c)}else B9(this,g.ya(this.RA,a,b))};
g.h.jt=function(a,b){if(1==this.g){z9(this,"setAudioTrack",{videoId:a,audioTrackId:b.Sc.id});var c=y9(this);c.audioTrackId=b.Sc.id;A9(this,c)}else B9(this,g.ya(this.jt,a,b))};
g.h.wB=function(a,b){if(1==this.g){if(a&&b){var c=y9(this);g.kI(c,a,b);A9(this,c)}z9(this,"previous")}else B9(this,g.ya(this.wB,a,b))};
g.h.vB=function(a,b){if(1==this.g){if(a&&b){var c=y9(this);g.kI(c,a,b);A9(this,c)}z9(this,"next")}else B9(this,g.ya(this.vB,a,b))};
g.h.Gv=function(){1==this.g?z9(this,"dismissAutoplay"):B9(this,this.Gv)};
g.h.dispose=function(){if(3!=this.g){var a=this.g;this.g=3;this.Y("proxyStateChange",a,this.g)}x9.aa.dispose.call(this)};
g.h.V=function(){dwa(this);this.B=null;this.C.clear();w9(this,null);x9.aa.V.call(this)};
g.h.kt=function(a){if((a!=this.g||2==a)&&3!=this.g&&0!=a){var b=this.g;this.g=a;this.Y("proxyStateChange",b,a);if(1==a)for(;!this.C.isEmpty();)g.EB(this.C).apply(this);else 3==a&&this.dispose()}};
g.h.RO=function(a,b){this.Y(a,b)};
g.h.GM=function(a){if(!a)this.Sk(null),w9(this,null);else if(this.A.receiver.volume){a=this.A.receiver.volume;var b=y9(this),c=Math.round(100*a.level||0);if(b.volume!=c||b.muted!=a.muted)D9("Cast volume update: "+a.level+(a.muted?" muted":"")),b.volume=c,b.muted=!!a.muted,A9(this,b)}};
g.h.Sk=function(a){D9("Cast media: "+!!a);this.o&&this.o.removeUpdateListener(this.G);if(this.o=a)this.o.addUpdateListener(this.G),ewa(this),this.Y("remotePlayerChange")};
g.h.FM=function(a){a?(ewa(this),this.Y("remotePlayerChange")):this.Sk(null)};
g.h.TM=function(){var a=Xva();a&&w9(this,a)};
g.h.qc=function(a){g.aH("CP",a)};
var H9=[];g.h=qwa.prototype;g.h.Qh=function(a,b,c){var d=this.g;if(g.t(c)?c:this.o)d="https://"+this.domain+this.port+this.g;return g.Dg(d+a,b||{})};
g.h.ht=function(a,b,c,d){c={format:"JSON",method:"POST",context:this,timeout:5E3,withCredentials:!1,Mb:g.ya(this.gS,c,!0),onError:g.ya(this.fS,d),dd:g.ya(this.hS,d)};b&&(c.Qb=b,c.headers={"Content-Type":"application/x-www-form-urlencoded"});return g.KE(a,c)};
g.h.gS=function(a,b,c,d){b?a(d):a({text:c.responseText})};
g.h.fS=function(a,b){a(Error("Request error: "+b.status))};
g.h.hS=function(a){a(Error("request timed out"))};
g.p(J9,g.M);g.h=J9.prototype;g.h.V=function(){g.M.prototype.V.call(this);this.A.stop();this.B.stop();this.M();this.g.unsubscribe("proxyStateChange",this.Mz,this);this.g.unsubscribe("remotePlayerChange",this.Uk,this);this.g.unsubscribe("remoteQueueChange",this.Cn,this);this.g.unsubscribe("autoplayUpNext",this.mz,this);this.g.unsubscribe("previousNextChange",this.Jz,this);this.g.unsubscribe("nowAutoplaying",this.Cz,this);this.g.unsubscribe("autoplayDismissed",this.lz,this);this.g=this.F=null};
g.h.ly=function(a,b){for(var c=[],d=1;d<arguments.length;++d)c[d-1]=arguments[d];if(2!=this.g.g)if(M9(this)){if(1081!=y9(this.g).g||"control_seek"!=a)switch(a){case "control_toggle_play_pause":y9(this.g).wb()?this.g.pause():this.g.play();break;case "control_play":this.g.play();break;case "control_pause":this.g.pause();break;case "control_seek":this.J.Vi(c[0],c[1]);break;case "control_subtitles_set_track":N9(this,c[0]);break;case "control_set_audio_track":c=c[0],M9(this)&&this.g.jt(this.U.ia().videoId,
c)}}else switch(a){case "control_toggle_play_pause":case "control_play":case "control_pause":O9(this,this.U.getCurrentTime());break;case "control_seek":O9(this,c[0]);break;case "control_subtitles_set_track":N9(this,c[0]);break;case "control_set_audio_track":c=c[0],M9(this)&&this.g.jt(this.U.ia().videoId,c)}};
g.h.EM=function(a){this.ly("control_subtitles_set_track",g.Tb(a)?null:a)};
g.h.cB=function(){var a=this.U.Bc("captions","track");g.Tb(a)||N9(this,a)};
g.h.Bz=function(a){if(M9(this)){this.g.unsubscribe("remotePlayerChange",this.Uk,this);var b=Math.round(a.volume);a=!!a.muted;var c=y9(this.g);if(b!=c.volume||a!=c.muted)this.g.setVolume(b,a),this.K.start();this.g.subscribe("remotePlayerChange",this.Uk,this)}};
g.h.SN=function(){g.Tb(this.C)||vwa(this,this.C);this.D=!1};
g.h.Mz=function(a,b){this.B.stop();2==b&&this.YA()};
g.h.Uk=function(){if(M9(this)){this.A.stop();var a=y9(this.g);switch(a.g){case 1081:L9(this,new g.WJ(8));break;case 1:this.XA();L9(this,new g.WJ(8));break;case 1083:case 3:L9(this,new g.WJ(9));break;case 0:L9(this,new g.WJ(2));this.J.stop();K9(this,this.U.ia().lengthSeconds);break;case 1082:L9(this,new g.WJ(4));break;case 2:L9(this,new g.WJ(4));K9(this,g.jI(a));break;case -1:L9(this,new g.WJ(64));break;case -1E3:a={errorCode:"mdx.remoteerror",message:g.T("YTP_MDX_PLAYER_ERROR")},L9(this,new g.WJ(128,
a))}var a=y9(this.g).o,b=this.C;(a||b?a&&b&&a.trackName==b.trackName&&a.languageCode==b.languageCode&&a.languageName==b.languageName&&a.format==b.format&&a.kind==b.kind:1)||(this.C=a,vwa(this,a));a=y9(this.g);-1==a.volume||Math.round(this.U.getVolume())==a.volume&&this.U.He()==a.muted||this.K.isActive()||this.nB()}else uwa(this)};
g.h.Jz=function(){this.U.Y("mdxpreviousnextchange")};
g.h.Cn=function(){M9(this)||uwa(this)};
g.h.yF=function(){this.g.Gv()};
g.h.mz=function(a){a&&(a=g.KE("/watch_queue_ajax",{method:"GET",Xc:{action_get_watch_queue_item:1,video_id:a},Mb:(0,g.A)(this.RP,this)}))&&(this.M=(0,g.A)(a.abort,a))};
g.h.RP=function(a,b){var c=new g.rQ({videoId:b.videoId,title:b.title,author:b.author,murlmq_webp:b.url});this.suggestion=c;this.U.Y("mdxautoplayupnext",c)};
g.h.Cz=function(a){(0,window.isNaN)(a)||this.U.Y("mdxnowautoplaying",a)};
g.h.lz=function(){this.U.Y("mdxautoplaycanceled")};
g.h.zK=function(a,b){-1==y9(this.g).g?O9(this,a):b&&this.g.xB(a)};
g.h.nB=function(){if(M9(this)){var a=y9(this.g);this.o.za(this.O);a.muted?this.U.mute():this.U.ug();this.U.setVolume(a.volume);this.O=this.o.T(this.U,"onVolumeChange",this.Bz)}};
g.h.XA=function(){this.A.stop();if(!this.g.na()){var a=y9(this.g);a.wb()&&L9(this,new g.WJ(8));K9(this,g.jI(a));this.A.start()}};
g.h.YA=function(){this.B.stop();this.A.stop();var a=this.g.B.getReconnectTimeout();2==this.g.g&&!(0,window.isNaN)(a)&&this.B.start()};g.p(P9,g.W);P9.prototype.B=function(a){xwa(this,a.state)};g.p(Q9,g.AV);g.h=Q9.prototype;g.h.create=function(){twa("embedded"==g.Y(this.g).g);this.D.push(g.dG("yt-remote-before-disconnect",this.BM,this));this.D.push(g.dG("yt-remote-connection-change",this.bP,this));this.D.push(g.dG("yt-remote-receiver-availability-change",this.Kz,this));this.D.push(g.dG("yt-remote-auto-connect",this.ZO,this));this.D.push(g.dG("yt-remote-receiver-resumed",this.YO,this));this.Kz()};
g.h.load=function(){this.g.rp();g.AV.prototype.load.call(this);this.C=new J9(this,this.g,this.o);var a=Vva(),a=a?a.currentTime:0,b=u9()?new x9(b9(),void 0):null;0==a&&b&&(a=g.jI(y9(b)));0!=a&&(this.J=a||0,this.g.Y("progresssync",a,void 0));wwa(this,this.F,this.F);g.m2(this.g.app,6)};
g.h.unload=function(){this.g.Y("mdxautoplaycanceled");this.B=this.A;g.Se(this.C,this.o);this.o=this.C=null;g.AV.prototype.unload.call(this);g.m2(this.g.app,5)};
g.h.V=function(){g.eG(this.D);g.AV.prototype.V.call(this)};
g.h.Mk=function(a,b){for(var c=[],d=1;d<arguments.length;++d)c[d-1]=arguments[d];this.loaded&&this.C.ly.apply(this.C,[].concat([a],c instanceof Array?c:rwa(g.ka(c))))};
g.h.kG=function(){return this.loaded?this.C.suggestion:null};
g.h.Ff=function(){return this.o?y9(this.o).Ff:!1};
g.h.hasNext=function(){return this.o?y9(this.o).hasNext:!1};
g.h.getCurrentTime=function(){return this.J};
g.h.nK=function(){var a=y9(this.o),b=this.g.ia();return{allowSeeking:this.g.Kd(),clipEnd:b.clipEnd,clipStart:b.clipStart,current:this.getCurrentTime(),displayedStart:-1,duration:a.getDuration(),ingestionTime:a.F?a.C+((0,g.G)()-a.A)/1E3:a.C,isPeggedToLive:1>=(a.F?a.B+((0,g.G)()-a.A)/1E3:a.B)-this.getCurrentTime(),loaded:a.K,seekableEnd:a.F?a.B+((0,g.G)()-a.A)/1E3:a.B,seekableStart:0<a.D?a.D+((0,g.G)()-a.A)/1E3:a.D}};
g.h.oK=function(){this.o&&this.o.vB()};
g.h.pK=function(){this.o&&this.o.wB()};
g.h.BM=function(a){1==a&&(this.K=this.o?y9(this.o):null)};
g.h.bP=function(){var a=u9()?new x9(b9(),void 0):null;if(a){var b=this.B;this.loaded&&this.unload();this.o=a;this.K=null;b.key!=this.A.key&&(this.B=b,this.load())}else g.Re(this.o),this.o=null,this.loaded&&(this.unload(),(a=this.K)&&a.videoId==this.g.ia().videoId&&this.g.Nx(a.videoId,g.jI(a)));this.g.Y("videodatachange","newdata",this.g.ia(),3)};
g.h.Kz=function(){this.G=[this.A].concat(swa());var a=G9()||this.A;R9(this,a);this.g.ya("onMdxReceiversChange")};
g.h.ZO=function(){var a=G9();R9(this,a)};
g.h.YO=function(){this.B=G9()};
g.h.mK=function(a,b){switch(a){case "casting":return this.loaded;case "receivers":return this.G;case "currentReceiver":return b&&("cast-selector-receiver"==b.key?I9():R9(this,b)),this.loaded?this.B:this.A;case "quickCast":return 2==this.G.length&&"cast-selector-receiver"==this.G[1].key?(b&&I9(),!0):!1}};
g.h.qK=function(){z9(this.o,"sendDebugCommand",{debugCommand:"stats4nerds "})};
g.h.Ad=function(){return!1};window._exportCheck==g.Ba&&g.v("ytmod.player.remote",Q9,void 0);})(_yt_player);
