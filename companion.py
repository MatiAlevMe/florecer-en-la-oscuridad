import pygame

class Companion:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.speed = 3  # Slower than the player for now
        # Load Sprites - Similar to Player, but for the Companion
        self.idle_frames = {
            "down": self.load_sprites("Assets/Sprites/Companion/Idle/idle_down.png"),
            "up": self.load_sprites("Assets/Sprites/Companion/Idle/idle_up.png"),
            "left_down": self.load_sprites("Assets/Sprites/Companion/Idle/idle_left_down.png"),
            "left_up": self.load_sprites("Assets/Sprites/Companion/Idle/idle_left_up.png"),
            "right_down": self.load_sprites("Assets/Sprites/Companion/Idle/idle_right_down.png"),
            "right_up": self.load_sprites("Assets/Sprites/Companion/Idle/idle_right_up.png"),
        }
        self.walk_frames = {
            "down": self.load_sprites("Assets/Sprites/Companion/Walk/walk_down.png"),
            "up": self.load_sprites("Assets/Sprites/Companion/Walk/walk_up.png"),
            "left_down": self.load_sprites("Assets/Sprites/Companion/Walk/walk_left_down.png"),
            "left_up": self.load_sprites("Assets/Sprites/Companion/Walk/walk_left_up.png"),
            "right_down": self.load_sprites("Assets/Sprites/Companion/Walk/walk_right_down.png"),
            "right_up": self.load_sprites("Assets/Sprites/Companion/Walk/walk_right_up.png"),
        }

        self.current_frames = self.idle_frames["down"]
        self.current_frame = 0
        self.image = self.current_frames[self.current_frame]
        self.rect = self.image.get_rect(center=(self.x, self.y))
        self.animation_speed = 0.15  # Adjust as needed
        self.animation_timer = 0
        self.is_moving = False  #  Add is_moving for consistency
        self.last_direction = "down" # Add last_direction for consistency

    def load_sprites(self, image_path):
        sprite_sheet = pygame.image.load(image_path)
        frames = []
        for i in range(8):
            frame = sprite_sheet.subsurface(pygame.Rect(i * 48, 0, 48, 64))
            frames.append(frame)
        return frames

    def draw(self, screen):
        self.animation_timer += 1
        if self.animation_timer >= 60 * self.animation_speed:
            self.animation_timer = 0
            self.current_frame = (self.current_frame + 1) % len(self.current_frames)
            self.image = self.current_frames[self.current_frame]
        screen.blit(self.image, self.rect)

    def update(self):
        # Placeholder for movement and AI logic.  We'll add follow behavior later.
        pass

    def handle_input(self):
        # Placeholder.  Companion input will be handled *after* it's unlocked.
        pass
