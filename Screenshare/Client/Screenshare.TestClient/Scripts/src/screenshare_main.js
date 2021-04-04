$(function () {
	window.baseUrl = 'https://localhost:44383';
	window.areEventsAttached = false;

	// Create master and send body to server
	$('#connect').click(onClickedConnect);
});

function onClickedConnect() {
	let screenshot = document.documentElement;
	serializeInputs(screenshot);
	serializeScrollable(screenshot);
	let body = screenshot.children[1].innerHTML;

	var headContent = document.getElementsByTagName('head')[0];
	//serializeInputs(headContent);
	let head = headContent.innerHTML;

	let height = $(window).height();
	let width = $(window).width();

	if (sessionStorage.getItem('masterGuid') == null) {
		$.ajax({
			type: 'POST',
			url: window.baseUrl + '/api/Screencast/RequestConnection',
			dataType: 'json',
			data: {
				'Body': body,
				'Head': head,
				'Width': width,
				'Height': height
			},
			success: onMasterGuidGenerated
		});
	}
}

function onMasterGuidGenerated(apiResponse) {
	sessionStorage.setItem('masterGuid', apiResponse.GUID);
	$('#slaveData').text(`${apiResponse.URL}`);
	let masterGuid = apiResponse.GUID;

	$.connection.hub.url = window.baseUrl + '/signalr';
	let screencasting = $.connection.screencastingHub;

	$.connection.hub.qs = 'uuid=' + masterGuid + '&master=true';
	$.connection.hub.logging = true;
	/*	screencasting.client.generatedUrlFromServer = generatedUrlFromServer;*/

	if ($.connection.hub && $.connection.hub.state === $.signalR.connectionState.disconnected) {
		$.connection.hub.start().done(function () {
			console.log('---Connected to signalR server via WebSockets');
			/*			screencasting.server.generateUrl(masterGuid, window.baseUrl);*/
		});
	}

	//function generatedUrlFromServer(url) {
	//	$('#slaveData').text(`Sharing data for ${url}`);
	//}
}

function serializeInputs(ele) {
	var _self = this;

	if (!ele || !ele.nodeName) {
		return;
	}

	switch (ele.nodeName) {
		default:
			if (ele.children) {
				for (var a in ele.children) {
					serializeInputs(ele.children[a])
				}
			}
			break;
		case 'SCRIPT':
			ele.parentNode.removeChild(ele);
			break;
		case 'INPUT':
			switch (ele.type) {
				default:
					ele.setAttribute('value', ele.value);
					break;
				case 'checkbox':
				case 'radio':
					if (ele.checked) {
						ele.setAttribute('checked', 'checked');
					}
					else {
						ele.removeAttribute('checked');
					}
					break;
				case 'file':
					break;
				case 'password':
					//Do not send password values...
					// ele.setAttribute('value', ele.value);
					break;
			}
			break;
		case 'TEXTAREA':
			ele.innerText = ele.value;
			break;
		case 'SELECT':
			switch (ele.type) {
				case 'select-one':
					for (var i = ele.options.length - 1; i >= 0; i--) {
						if (ele.options[i].selected) {
							ele.options[i].setAttribute('selected', 'selected');
						}
						else {
							ele.options[i].removeAttribute('selected');
						}
					}
					break;
				case 'select-multiple':
					for (i = ele.options.length - 1; i >= 0; i--) {
						if (ele.options[i].selected) {
							ele.options[i].setAttribute('selected', 'selected');
						}
						else {
							ele.options[i].removeAttribute('selected');
						}
					}
					break;
			}
			break;
	}
}

function serializeScrollable(ele) {
	var _self = this;
	if (!ele || !ele.nodeName) {
		return;
	}
	if (ele.scrollTop || ele.scrollLeft || ele.getAttribute('data-scroll')) {
		ele.setAttribute('data-scroll', `${~~(ele.scrollTop)},${~~(ele.scrollLeft)}`);
	}
	if (ele.children) {
		for (var a in ele.children) {
			serializeScrollable(ele.children[a])
		}
	}
}