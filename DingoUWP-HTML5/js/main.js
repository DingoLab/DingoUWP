(function() {
  'use strict';
  var app = WinJS.Application;
  var activation = Windows.ApplicationModel.Activation;
  app.onactivated = function (args) {
    if (args.detail.kind === activation.ActivationKind.launch) {
      if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
        // TODO: 已经新启动此应用程序。请在此初始化你的应用程序。
      } else {
        // TODO: 该应用程序已从挂起状态被重新激活。
        // 在此处还原应用程序的状态。
      }
      args.setPromise(WinJS.UI.processAll().then(function() {
        // TODO: 此处显示你的代码。
      }));
    }
  };
  app.oncheckpoint = function (args) {
    // TODO: 此应用程序将被挂起。请在此保存需要挂起中需要保存的任何状态。
    //你可以使用 WinJS.Application.sessionState 对象，该对象在挂起中会自动保存和还原。
    //如果需要在应用程序被挂起之前完成异步操作，请调用 args.setPromise()。
  };
  app.start();
}());
