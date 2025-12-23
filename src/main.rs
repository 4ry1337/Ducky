pub mod config;
pub mod dev_tools;
pub mod entities;

use bevy::prelude::*;

use crate::{config::GameConfig, dev_tools::fps_overlay};

fn main() {
    let config = GameConfig::new("config").expect("Failed to read configuration.");
    let mut app = App::new();
    app.add_plugins(DefaultPlugins).add_systems(Startup, camera);
    if config.dev_tools {
        fps_overlay(&mut app);
    }
    app.run();
}

fn camera(mut commands: Commands) {
    commands.spawn((
        Camera3d::default(),
        Transform::from_xyz(-2.5, 4.5, 9.0).looking_at(Vec3::ZERO, Vec3::Y),
    ));
}
