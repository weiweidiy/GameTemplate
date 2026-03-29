// uni-shim.js
(function() {
    if (typeof uni !== 'undefined') return;
    var wxApi = typeof wx !== 'undefined' ? wx : null;
    if (!wxApi) {
        console.error('wx is not defined');
        return;
    }
    globalThis.uni = {
        request: function(options) {
            wxApi.request({
                url: options.url,
                data: options.data,
                header: options.header,
                method: options.method,
                dataType: options.responseType === 'arraybuffer' ? 'arraybuffer' : undefined,
                responseType: options.responseType,
                success: options.success,
                fail: options.fail,
                complete: options.complete
            });
        },
        connectSocket: function(options) {
            return wxApi.connectSocket({
                url: options.url,
                header: options.header,
                protocols: options.protocols,
                success: options.success,
                fail: options.fail,
                complete: options.complete
            });
        },
        // 如有其他用到的 API 可以继续补充

        // ===== 存储 API =====
        setStorageSync: function (key, data) {
            wxApi.setStorageSync(key, data);
        },
        getStorageSync: function (key) {
            return wxApi.getStorageSync(key);
        },
        removeStorageSync: function (key) {
            wxApi.removeStorageSync(key);
        },
        clearStorageSync: function () {
            wxApi.clearStorageSync();
        },
        setStorage: function (options) {
            wxApi.setStorage({
                key: options.key,
                data: options.data,
                success: options.success,
                fail: options.fail,
                complete: options.complete
            });
        },
        getStorage: function (options) {
            wxApi.getStorage({
                key: options.key,
                success: options.success,
                fail: options.fail,
                complete: options.complete
            });
        },
        removeStorage: function (options) {
            wxApi.removeStorage({
                key: options.key,
                success: options.success,
                fail: options.fail,
                complete: options.complete
            });
        },
        clearStorage: function (options) {
            wxApi.clearStorage(options);
        },
        // ===== 系统信息 =====
        getSystemInfoSync: function () {
            return wxApi.getSystemInfoSync();
        },
        getSystemInfo: function (options) {
            wxApi.getSystemInfo(options);
        },

        // ===== 网络状态 =====
        getNetworkType: function (options) {
            wxApi.getNetworkType(options);
        },
        onNetworkStatusChange: function (callback) {
            wxApi.onNetworkStatusChange(callback);
        },
        // ===== UI 交互 =====
        showToast: function (options) {
            wxApi.showToast(options);
        },
        showModal: function (options) {
            wxApi.showModal(options);
        },
        showLoading: function (options) {
            wxApi.showLoading(options);
        },
        hideLoading: function () {
            wxApi.hideLoading();
        },
        showActionSheet: function (options) {
            wxApi.showActionSheet(options);
        },

        // ===== 其他常用 =====
        navigateTo: function (options) {
            wxApi.navigateTo(options);
        },
        redirectTo: function (options) {
            wxApi.redirectTo(options);
        },
        navigateBack: function (options) {
            wxApi.navigateBack(options);
        },
        switchTab: function (options) {
            wxApi.switchTab(options);
        },
        reLaunch: function (options) {
            wxApi.reLaunch(options);
        },
        getLocation: function (options) {
            wxApi.getLocation(options);
        },
        chooseImage: function (options) {
            wxApi.chooseImage(options);
        },
        previewImage: function (options) {
            wxApi.previewImage(options);
        },
        // 文件操作
        saveFile: function (options) {
            wxApi.saveFile(options);
        },
        getFileInfo: function (options) {
            wxApi.getFileInfo(options);
        },
        // 振动
        vibrateLong: function (options) {
            wxApi.vibrateLong(options);
        },
        vibrateShort: function (options) {
            wxApi.vibrateShort(options);
        },
    };
})();