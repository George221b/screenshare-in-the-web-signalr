$(function () {
	const urlParams = new URLSearchParams(window.location.search);
	const masterGuid = urlParams.get('masterGuid');

	if (masterGuid != null) {
		createEmptyHtmlTemplateInIframe();

		let screencasting = $.connection.screencastingHub;

		screencasting.client.receiveInitialMasterBody = receiveInitialMasterBody;
		screencasting.client.receiveMasterBody = receiveMasterBody;
		screencasting.client.receiveMasterScroll = receiveMasterScroll;
		screencasting.client.receiveMasterMouse = receiveMasterMouse;

		$.connection.hub.qs = 'uuid=' + masterGuid + '&master=false';
		$.connection.hub.logging = true;

		$.connection.hub.start().done(function () {
			console.log('---Connected to signalR server via WebSockets');
			screencasting.server.receiveBody(masterGuid);
		});
	}
});

function receiveInitialMasterBody(body, head, width, height) {
	let iframe = document.getElementById('screenshare');
	let iframeWindow = iframe.contentWindow;

	$("#screenshare").width(width);
	$("#screenshare").height(height);

	iframeWindow.document.head.innerHTML = head;
	iframeWindow.document.body.innerHTML = body;
	iframeWindow.document.body.innerHTML.replace('</body>', '<style> *{animation-duration: 0s !important} html, body {user-select: none !important; pointer-event: none !important;}</style></body>');
}

function receiveMasterBody(body, width, height) {
	let iframe = document.getElementById('screenshare');
	let iframeWindow = iframe.contentWindow;

	$("#screenshare").width(width);
	$("#screenshare").height(height);

	iframeWindow.document.body.innerHTML = body;
	iframeWindow.document.body.innerHTML.replace('</body>', '<style> *{animation-duration: 0s !important} html, body {user-select: none !important; pointer-event: none !important;}</style></body>');

	let jBody = $(iframeWindow.document.body);
	let scrollElems = jBody.find('[data-scroll]');
	for (let a = 0; a < scrollElems.length; a++) {
		let scrollPositions = scrollElems[a].getAttribute('data-scroll').split(',').map(x => +x);
		$(scrollElems[a]).scrollTop(scrollPositions[0]);
	}
}

function receiveMasterScroll(xPercent, yPercent) {
	let iframe = document.getElementById('screenshare');
	let iframeWindow = iframe.contentWindow;

	iframeWindow.scrollTo(xPercent, yPercent);
}

function receiveMasterMouse(mousePositionX, mousePositionY) {
	$('#cursor')
		.css('position', 'absolute')
		.css('top', `${mousePositionY}px`)
		.css('left', `${mousePositionX}px`)
		.css('z-index', '1');
}

function createEmptyHtmlTemplateInIframe() {
	let iframeElement = document.getElementsByTagName("iframe")[0];
	iframeElement.id = 'screenshare';
	iframeElement.setAttribute('scrolling', 'no');
	iframeElement.setAttribute('scrolling', 'no');
	iframeElement.setAttribute('frameborder', '0');

	var ifrm = document.getElementById('screenshare');
	ifrm = ifrm.contentWindow || ifrm.contentDocument.document || ifrm.contentDocument;
	ifrm.document.open();
	ifrm.document.write('<!DOCTYPE html>');
	ifrm.document.write('<html>');
	ifrm.document.write('<head></head>');
	ifrm.document.write('<body><div></div></body>');
	ifrm.document.write('</html>');
	ifrm.document.close();
}