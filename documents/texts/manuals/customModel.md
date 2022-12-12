# Pose AIで任意のモデルを使用する

基本的に[こちら](https://github.com/PoseAI/PoseCameraAPI/blob/main/FrameworkDocumentation/RigRetargeting.md)を見ながら行う。

- `6. Leave both models' animator controllers set to none if they are in same position. Otherwise use the Unity animator controllers to get them in the same position (i.e. the t-pose)`については、ロボットとカスタムモデルの両方のAnimator ControllerをIdleに設定する。
- `standard model`、`custom model`ともに、すべての関節が用意されているものでないとエラーが起きる。