import pygame
import sys
from player import Player
from companion import Companion
from map import Map
from dialogue import DialogueManager

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
    game_map = Map(100, 100, TILE_SIZE)  # Tamaño del mapa más grande
    dialogue_manager = DialogueManager(screen)
    clock = pygame.time.Clock()

    # Estados del juego
    STATE_EXPLORING = 0
    STATE_DIALOGUE = 1
    current_state = STATE_EXPLORING

    # Diálogos (estructura de ejemplo)
    dialogues = {
        "start": [
            "Protagonista: ¿Dónde estoy?",
            "Compañero: Parece un lugar oscuro y desconocido.",
            "Protagonista: Tengo miedo...",
            ["Avanzar con valentía", "Quedarse paralizada"],
            "Compañero: No te preocupes, estoy aquí contigo."
        ],
        "after_battle1": [
            "Compañero: ¡Lo lograste! Eres más fuerte de lo que pensabas.",
            "Protagonista: Gracias... por estar a mi lado."
        ],
        #Añade mas dialogos aqui
    }
    
    camera_x = 0
    camera_y = 0

    running = True
    while running:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False
            if event.type == pygame.KEYDOWN:
                if current_state == STATE_DIALOGUE:
                    if event.key == pygame.K_RETURN:
                        dialogue_manager.next_line()
                        if dialogue_manager.current_dialogue is None:
                            current_state = STATE_EXPLORING  # Vuelve a la exploración

        if current_state == STATE_EXPLORING:
            player.handle_input()
            companion.update(player.x, player.y)
            #Centrar la camara
            camera_x = player.x - SCREEN_WIDTH // 2
            camera_y = player.y - SCREEN_HEIGHT // 2

            # --- LOGICA DE PROGRESO (ejemplo) ---
            if player.x > 100 and dialogue_manager.current_dialogue is None:  # Evento de inicio
                dialogue_manager.start_dialogue(dialogues["start"])
                current_state = STATE_DIALOGUE

            #Ejemplo de iluminacion
            game_map.illuminate(player.x // TILE_SIZE, player.y // TILE_SIZE, 5, 1)


        screen.fill((0, 0, 0))  # Llena la pantalla de negro
        game_map.draw(screen, camera_x, camera_y) #Dibuja mapa
        player.draw(screen)
        companion.draw(screen)

        if current_state == STATE_DIALOGUE:
            dialogue_manager.update()
            dialogue_manager.draw()

        pygame.display.flip()
        clock.tick(60)

    pygame.quit()
    sys.exit()

if __name__ == "__main__":
    main()
