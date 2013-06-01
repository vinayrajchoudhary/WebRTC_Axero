var startCall = document.getElementById('startcall');
if (startCall) startCall.onclick = createButtonClickHandler;
var localStream = null;
var channelReady = true;
var started = false, initiator = true;
mediaConstraints = {
    optional: [],
    mandatory: {
        OfferToReceiveAudio: true,
        OfferToReceiveVideo: true
    }
};
window.moz = !!navigator.mozGetUserMedia;
function createButtonClickHandler() {
    
    
    getUserMedia({
        video: document.getElementById("video"),
        constraints: {},
        onsuccess: function (localMediaStream) {
           localStream = localMediaStream;
           maybeStart();
           
        },
        onerror: function (event) { console.log("error while getting local user media");}
    });
}
 
    function maybeStart() {
   // if (!started && localStream && channelReady) {
        // ...
      
        createPeerConnection();
        // ...

        if (localStream) { console.log("localstream not null");}
            pc.addStream(localStream);
        started = true;
        // Caller initiates offer to peer.
      //  if (initiator)
            doCall();
    //}
}
    function createPeerConnection() {
        var pc_config = {"iceServers": [{"url": "stun:stun.l.google.com:19302"}]};
        var w = window,
            RTCPeerConnection = w.mozRTCPeerConnection || w.webkitRTCPeerConnection,
           SessionDescription = w.mozRTCSessionDescription || w.RTCSessionDescription,
           IceCandidate = w.mozRTCIceCandidate || w.RTCIceCandidate;

        try {
            pc = new RTCPeerConnection(pc_config);
            pc.onicecandidate = onIceCandidate;
            console.log("Created RTCPeerConnnection with config:\n" + "  \"" +
              JSON.stringify(pc_config) + "\".");
        } catch (e) {
            console.log("Failed to create PeerConnection, exception: " + e.message);
            alert("Cannot create RTCPeerConnection object; WebRTC is not supported by this browser.");
            return;
        }
        pc.addStream(localStream);
        pc.onconnecting = onSessionConnecting;
        pc.onopen = onSessionOpened;
        pc.onaddstream = onRemoteStreamAdded;
        pc.onremovestream = onRemoteStreamRemoved;
    }
    function onIceCandidate(event) {
        if (event.candidate) {
            sendMessage({
                type: 'candidate',
                label: event.candidate.sdpMLineIndex,
                id: event.candidate.sdpMid,
                candidate: event.candidate.candidate
            });
        } else {
            console.log("End of candidates.");
        }
    }
    function onSessionConnecting() { }
    function onSessionOpened() { }
    function onRemoteStreamAdded() {
        alert("yay!!!! remote stream is here");
    }
    function onRemoteStreamRemoved() { }
    function doCall() {
        console.log("Sending offer to peer.");
        pc.createOffer(function (sessionDescription) {
            pc.setLocalDescription(sessionDescription,
                                function () { console.log("succesfully set local description") }
                                , function () { console.log("set local description failed");});
            if (moz) returnSDP();
            sendMessage( sessionDescription );
 
        }, function () { alert("some error creating offer");}, mediaConstraints);
    }
    function setLocalAndSendMessage(sessionDescription) {
        // Set Opus as the preferred codec in SDP if Opus is present.
        sessionDescription.sdp = preferOpus(sessionDescription.sdp);
        pc.setLocalDescription(sessionDescription,
                                function () { console.log("succesfully set local description")});
        if (moz) console.log("moz sharing local description "+ sessionDescription);
    }
    function sendMessage(message) {
        var msgString = JSON.stringify(message);
        console.log('C->S: ' + msgString);
     
        chat.server.send(msgString);

    }

    function doAnswer() {
        console.log("Creating answer to peer.");
        pc.createAnswer(function (sessionDescription) {

            pc.setLocalDescription(sessionDescription);
            if (moz) returnSDP();
            sendMessage(sessionDescription);

        }, function () { console.log("failure while creating answer")}, mediaConstraints);
        
    }
    function getUserMedia(options) {
        var n = navigator, media;
        n.getMedia = n.webkitGetUserMedia || n.mozGetUserMedia;
        n.getMedia( {
            audio: true,
            video: true
        }, streaming, options.onerror || function (e) {
            console.error(e);
        });

        function streaming(stream) {
            var video = options.video;
            console.log("streaming");
            if (video) {
                video[moz ? 'mozSrcObject' : 'src'] = moz ? stream : window.webkitURL.createObjectURL(stream);

                video.play();
            }
            options.onsuccess && options.onsuccess(stream);
            media = stream;
        }

        return media;
    }
    function returnSDP() {
        alert("alert this is mozilla");
    }
    function r(a) {
        var sdp = new RTCSessionDescription({ "type": "offer", "sdp": a });
       pc.setRemoteDescription(sdp  );
        alert("here it shoul");
        console.log(pc);

    }