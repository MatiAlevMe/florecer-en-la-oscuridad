import pygame
import random
import noise  # Necesitarás instalar: pip install noise

class Map:
    def __init__(self, width, height, tile_size):
        self.width = width
        self.height = height
        self.tile_size = tile_size
        self.base_map = self.generate_base_map()
        self.color_map = self.generate_color_map() # Mapa de colores
        self.tileset = self.load_tileset() # Carga los tiles

    def generate_base_map(self):
        # Genera un mapa base usando Perlin Noise
        world_map = [[0 for _ in range(self.width)] for _ in range(self.height)]
        scale = 0.1  # Ajusta la escala para la "suavidad" del terreno
        for x in range(self.width):
            for y in range(self.height):
                value = noise.pnoise2(x * scale, y * scale, octaves=4, persistence=0.5, lacunarity=2.0)
                # Ajusta los umbrales para determinar el tipo de tile
                if value < -0.2:
                    world_map[y][x] = 0  # Agua/Vacío
                elif value < 0.1:
                    world_map[y][x] = 1  # Tierra/Pasto
                else:
                    world_map[y][x] = 2  # Montaña/Roca

        return world_map

    def generate_color_map(self):
        # Inicialmente, todo el mapa tiene el color base (oscuro)
        return [[0 for _ in range(self.width)] for _ in range(self.height)]

    def load_tileset(self):
        # Carga las imágenes de los tiles (debes tener los archivos correspondientes)
        tileset = {
            0: {  # Tiles oscuros/base
                0: pygame.image.load("Assets/Sprites/Tiles/water_tile_dark.png"),
                1: pygame.image.load("Assets/Sprites/Tiles/grass_tile_dark.png"),
                2: pygame.image.load("Assets/Sprites/Tiles/rock_tile_dark.png"),
            },
            1: {  # Tiles iluminados
                0: pygame.image.load("Assets/Sprites/Tiles/water_tile_light.png"),
                1: pygame.image.load("Assets/Sprites/Tiles/grass_tile_light.png"),
                2: pygame.image.load("Assets/Sprites/Tiles/rock_tile_light.png"),
            },
            # Puedes añadir más niveles de iluminación si quieres
        }
        return tileset

    def draw(self, screen, camera_x, camera_y):
        # Dibuja el mapa en la pantalla, teniendo en cuenta la cámara
        start_x = max(0, int(camera_x // self.tile_size))
        start_y = max(0, int(camera_y // self.tile_size))
        end_x = min(self.width, int((camera_x + screen.get_width()) // self.tile_size + 2))
        end_y = min(self.height, int((camera_y + screen.get_height()) // self.tile_size + 2))

        for x in range(start_x, end_x):
            for y in range(start_y, end_y):
                tile_type = self.base_map[y][x]
                color_level = self.color_map[y][x]
                tile_image = self.tileset[color_level][tile_type]
                screen.blit(tile_image, (x * self.tile_size - camera_x, y * self.tile_size - camera_y))

    def illuminate(self, center_x, center_y, radius, level):
        # Ilumina el área alrededor de un punto (x, y) con un radio y nivel de iluminación
        start_x = max(0, int(center_x - radius))
        start_y = max(0, int(center_y - radius))
        end_x = min(self.width, int(center_x + radius + 1))
        end_y = min(self.height, int(center_y + radius + 1))

        for x in range(start_x, end_x):
            for y in range(start_y, end_y):
                distance = ((x - center_x) ** 2 + (y - center_y) ** 2) ** 0.5
                if distance <= radius:
                    self.color_map[y][x] = min(level, 1)  # Asegura que no exceda el nivel máximo

    def is_walkable(self, x, y):
        # Ejemplo simple: el agua no es caminable
        if 0 <= x < self.width and 0 <= y < self.height:
            return self.base_map[y][x] != 0
        return False #Fuera de los limites no es walkable
