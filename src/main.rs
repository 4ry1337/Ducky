use bevy::{
    dev_tools::fps_overlay::{FpsOverlayConfig, FpsOverlayPlugin, FrameTimeGraphConfig},
    prelude::*,
    text::FontSmoothing
};

struct OverlayColor;

impl OverlayColor {
    const RED: Color = Color::srgb(1.0, 0.0, 0.0);
    const GREEN: Color = Color::srgb(0.0, 1.0, 0.0);
}

fn main() {
    App::new()
        .add_plugins((
            DefaultPlugins,
            FpsOverlayPlugin {
                config: FpsOverlayConfig {
                    text_config:             TextFont {
                        font_size: 42.0,
                        font: default(),
                        font_smoothing: FontSmoothing::default(),
                        ..default()
                    },
                    text_color:              OverlayColor::GREEN,
                    refresh_interval:        core::time::Duration::from_millis(100),
                    enabled:                 true,
                    frame_time_graph_config: FrameTimeGraphConfig {
                        enabled:    true,
                        min_fps:    30.0,
                        target_fps: 144.0
                    }
                }
            }
        ))
        .add_systems(Startup, setup)
        .add_systems(Update, customize_config)
        .run();
}

/// set up a simple 3D scene
fn setup(
    mut commands: Commands,
    mut meshes: ResMut<Assets<Mesh>>,
    mut materials: ResMut<Assets<StandardMaterial>>
) {
    // circular base
    commands.spawn((
        Mesh3d(meshes.add(Circle::new(4.0))),
        MeshMaterial3d(materials.add(Color::WHITE)),
        Transform::from_rotation(Quat::from_rotation_x(-std::f32::consts::FRAC_PI_2))
    ));
    // cube
    commands.spawn((
        Mesh3d(meshes.add(Cuboid::new(1.0, 1.0, 1.0))),
        MeshMaterial3d(materials.add(Color::srgb_u8(124, 144, 255))),
        Transform::from_xyz(0.0, 0.5, 0.0)
    ));
    // light
    commands.spawn((
        PointLight {
            shadows_enabled: true,
            ..default()
        },
        Transform::from_xyz(4.0, 8.0, 4.0)
    ));
    // camera
    commands.spawn((
        Camera3d::default(),
        Transform::from_xyz(-2.5, 4.5, 9.0).looking_at(Vec3::ZERO, Vec3::Y)
    ));

    // Instruction text
    commands.spawn((
        Text::new(concat!(
            "Press 1 to toggle the overlay color.\n",
            "Press 2 to decrease the overlay size.\n",
            "Press 3 to increase the overlay size.\n",
            "Press 4 to toggle the text visibility.\n",
            "Press 5 to toggle the frame time graph."
        )),
        Node {
            position_type: PositionType::Absolute,
            bottom: px(12),
            left: px(12),
            ..default()
        }
    ));
}

fn customize_config(input: Res<ButtonInput<KeyCode>>, mut overlay: ResMut<FpsOverlayConfig>) {
    if input.just_pressed(KeyCode::Digit1) {
        if overlay.text_color == OverlayColor::GREEN {
            overlay.text_color = OverlayColor::RED;
        } else {
            overlay.text_color = OverlayColor::GREEN;
        }
    }
    if input.just_pressed(KeyCode::Digit2) {
        overlay.text_config.font_size -= 2.0;
    }
    if input.just_pressed(KeyCode::Digit3) {
        overlay.text_config.font_size += 2.0;
    }
    if input.just_pressed(KeyCode::Digit4) {
        overlay.enabled = !overlay.enabled;
    }
    if input.just_released(KeyCode::Digit5) {
        overlay.frame_time_graph_config.enabled = !overlay.frame_time_graph_config.enabled;
    }
}
