import pygame
import math  # Import the math module

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
        self.is_moving = False
        self.last_direction = "down"
        self.follow_distance = 100  # Distance at which the companion starts following

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

    def update(self, player_x, player_y):
        # Follow behavior
        distance = math.sqrt((player_x - self.x)**2 + (player_y - self.y)**2)
        self.is_moving = False

        if distance > self.follow_distance:
            self.is_moving = True
            dx = player_x - self.x
            dy = player_y - self.y

            # Normalize the vector
            angle = math.atan2(dy, dx)  # Use atan2 for correct angle
            move_x = self.speed * math.cos(angle)
            move_y = self.speed * math.sin(angle)

            self.x += move_x
            self.y += move_y

            # Determine direction for animation
            if abs(move_x) > abs(move_y):  # More horizontal movement
                if move_x > 0:
                    self.last_direction = "right_down" if move_y>=0 else "right_up"
                else:
                    self.last_direction = "left_down" if move_y>=0 else "left_up"
            else:  # More vertical movement or equal
                if move_y > 0:
                    self.last_direction = "down"
                else:
                    self.last_direction = "up"

            self.rect.center = (self.x, self.y)

        # Animation selection
        if self.is_moving:
            self.current_frames = self.walk_frames[self.last_direction]
        else:
            self.current_frames = self.idle_frames[self.last_direction]


    def handle_input(self):
        # Placeholder.  Companion input will be handled *after* it's unlocked.
        pass
