import pygame
import sys
from player import Player
from companion import Companion

# Constantes
SCREEN_WIDTH = 800
SCREEN_HEIGHT = 600
TILE_SIZE = 48

def main():
    pygame.init()

    screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
    pygame.display.set_caption("Florecer en la Oscuridad")

    player = Player(SCREEN_WIDTH // 2, SCREEN_HEIGHT // 2)
    companion = Companion(SCREEN_WIDTH // 2 + 50, SCREEN_HEIGHT // 2)
    clock = pygame.time.Clock()

    running = True
    while running:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False

        player.handle_input()
        companion.update(player.x, player.y)  # Pass player's position to companion
        screen.fill((0, 0, 0))
        player.draw(screen)
        companion.draw(screen)
        pygame.display.flip()
        clock.tick(60)

    pygame.quit()
    sys.exit()

if __name__ == "__main__":
    main()
