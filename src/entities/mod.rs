use bevy::ecs::component::Component;

#[derive(Component)]
pub struct Player;

#[derive(Component)]
pub struct Trap;

#[derive(Component)]
pub struct Health(u8);

impl Default for Health {
    fn default() -> Self {
        Self(3)
    }
}
