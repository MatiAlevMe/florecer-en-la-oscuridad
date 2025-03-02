import pygame

class Player:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.speed = 5
        # Carga de Sprites
        self.idle_frames = {
            "down": self.load_sprites("Assets/Sprites/Player/Idle/Idle_Down.png"),
            "up": self.load_sprites("Assets/Sprites/Player/Idle/Idle_Up.png"),
            "left_down": self.load_sprites("Assets/Sprites/Player/Idle/Idle_Left_Down.png"),
            "left_up": self.load_sprites("Assets/Sprites/Player/Idle/Idle_Left_Up.png"),
            "right_down": self.load_sprites("Assets/Sprites/Player/Idle/Idle_Right_Down.png"),
            "right_up": self.load_sprites("Assets/Sprites/Player/Idle/Idle_Right_Up.png"),
        }
        self.walk_frames = {
            "down": self.load_sprites("Assets/Sprites/Player/Walk/walk_Down.png"),
            "up": self.load_sprites("Assets/Sprites/Player/Walk/walk_Up.png"),
            "left_down": self.load_sprites("Assets/Sprites/Player/Walk/walk_Left_Down.png"),
            "left_up": self.load_sprites("Assets/Sprites/Player/Walk/walk_Left_Up.png"),
            "right_down": self.load_sprites("Assets/Sprites/Player/Walk/walk_Right_Down.png"),
            "right_up": self.load_sprites("Assets/Sprites/Player/Walk/walk_Right_Up.png"),
        }

        self.current_frames = self.idle_frames["down"] # Inicialmente, usa idle_down
        self.current_frame = 0
        self.image = self.current_frames[self.current_frame]
        self.rect = self.image.get_rect(center=(self.x, self.y))
        self.animation_speed = 0.1
        self.animation_timer = 0
        self.is_moving = False
        self.last_direction = "down" # Para recordar la última dirección


    def load_sprites(self, image_path):
        sprite_sheet = pygame.image.load(image_path)
        frames = []
        for i in range(8):
            frame = sprite_sheet.subsurface(pygame.Rect(i * 48, 0, 48, 64))
            frames.append(frame)
        return frames

    def handle_input(self):
        keys = pygame.key.get_pressed()
        self.is_moving = False  # Asume que no se mueve, a menos que se presione una tecla

        if keys[pygame.K_w]:
            self.y -= self.speed
            self.is_moving = True
            self.last_direction = "up"
        if keys[pygame.K_s]:
            self.y += self.speed
            self.is_moving = True
            self.last_direction = "down"
        if keys[pygame.K_a]:
            self.x -= self.speed
            self.is_moving = True
            self.last_direction = "left_down" if not keys[pygame.K_w] else "left_up" #Diagonales
        if keys[pygame.K_d]:
            self.x += self.speed
            self.is_moving = True
            self.last_direction = "right_down" if not keys[pygame.K_w] else "right_up" #Diagonales

        self.rect.center = (self.x, self.y)

        #Seleccion de animacion
        if self.is_moving:
            self.current_frames = self.walk_frames[self.last_direction]
        else:
            self.current_frames = self.idle_frames[self.last_direction]

    def draw(self, screen, camera_x, camera_y):
        self.animation_timer += 1
        if self.animation_timer >= 60 * self.animation_speed:
            self.animation_timer = 0
            self.current_frame = (self.current_frame + 1) % len(self.current_frames)
            self.image = self.current_frames[self.current_frame]
        screen.blit(self.image, (self.rect.x - camera_x, self.rect.y - camera_y))
