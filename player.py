import pygame

class Player:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.speed = 5
        #Carga de Sprites
        self.idle_frames = self.load_sprites("Assets/Sprites/Player/Idle/Idle.png")
        self.current_frame = 0
        self.image = self.idle_frames[self.current_frame]
        self.rect = self.image.get_rect(center=(self.x, self.y))
        self.animation_speed = 0.1  # Velocidad de la animación (ajusta según necesidad)
        self.animation_timer = 0


    def load_sprites(self, image_path):
        sprite_sheet = pygame.image.load(image_path)
        frames = []
        #Asumiendo 8 frames por sprite sheet y 48x64
        for i in range(8):
            frame = sprite_sheet.subsurface(pygame.Rect(i * 48, 0, 48, 64))
            frames.append(frame)
        return frames

    def handle_input(self):
        keys = pygame.key.get_pressed()
        if keys[pygame.K_w]:
            self.y -= self.speed
        if keys[pygame.K_s]:
            self.y += self.speed
        if keys[pygame.K_a]:
            self.x -= self.speed
        if keys[pygame.K_d]:
            self.x += self.speed

        self.rect.center = (self.x, self.y) #Actualiza el rect

    def draw(self, screen):
        #screen.blit(self.image, (self.x, self.y))
        #Animacion
        self.animation_timer += 1
        if self.animation_timer >= 60 * self.animation_speed:
            self.animation_timer = 0
            self.current_frame = (self.current_frame + 1) % len(self.idle_frames)
            self.image = self.idle_frames[self.current_frame]
        screen.blit(self.image, self.rect)


