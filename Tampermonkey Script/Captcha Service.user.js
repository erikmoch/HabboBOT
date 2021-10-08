// ==UserScript==
// @name         Captcha Service
// @namespace    mservice
// @version      1.0
// @description  no description
// @author       Moch
// @match        https://www.habbo.com.br/api/public/captcha*
// @grant        none
// ==/UserScript==

(function () {
    'use strict';

    var socket = new WebSocket("ws://127.0.0.1:8080");
    socket.onopen = () => {
        const urlParams = new URLSearchParams(window.location.search);
        if (urlParams.has("token") && urlParams.get("token") !== "") {
            socket.send(urlParams.get("token"));
        }
    }
})();