use bevy::{
    dev_tools::fps_overlay::{FpsOverlayConfig, FpsOverlayPlugin, FrameTimeGraphConfig},
    prelude::*,
    text::FontSmoothing,
};

pub fn fps_overlay(app: &mut App) {
    app.add_plugins(FpsOverlayPlugin {
        config: FpsOverlayConfig {
            text_config: TextFont {
                font_size: 42.0,
                font: default(),
                font_smoothing: FontSmoothing::default(),
                ..default()
            },
            text_color: Color::srgb(0.0, 1.0, 0.0),
            refresh_interval: core::time::Duration::from_millis(100),
            enabled: true,
            frame_time_graph_config: FrameTimeGraphConfig {
                enabled: true,
                min_fps: 30.0,
                target_fps: 144.0,
            },
        },
    })
    .add_systems(Startup, setup)
    .add_systems(Update, customize_config);
}

fn setup(mut commands: Commands) {
    commands.spawn((
        Text::new(concat!(
            "Press 1 to toggle the text visibility.\n",
            "Press 2 to toggle the frame time graph."
        )),
        Node {
            position_type: PositionType::Absolute,
            bottom: px(12),
            left: px(12),
            ..default()
        },
    ));
}

fn customize_config(input: Res<ButtonInput<KeyCode>>, mut overlay: ResMut<FpsOverlayConfig>) {
    if input.just_pressed(KeyCode::Digit1) {
        overlay.enabled = !overlay.enabled;
    }
    if input.just_released(KeyCode::Digit2) {
        overlay.frame_time_graph_config.enabled = !overlay.frame_time_graph_config.enabled;
    }
}
