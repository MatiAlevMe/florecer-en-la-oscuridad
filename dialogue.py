import pygame

class DialogueManager:
    def __init__(self, screen, font_size=30):
        self.screen = screen
        self.font = pygame.font.Font(None, font_size)  # Puedes usar una fuente específica
        self.text_color = (255, 255, 255)  # Blanco
        self.dialogue_box_color = (50, 50, 50)  # Gris oscuro
        self.dialogue_box_rect = pygame.Rect(50, screen.get_height() - 200, screen.get_width() - 100, 150)
        self.current_dialogue = None
        self.current_line_index = 0
        self.typing_speed = 120  # Caracteres por segundo.  Aumentado para mayor velocidad.
        self.current_typed_text = ""
        self.last_update_time = 0
        self.finished_typing = False
        self.options = []
        self.selected_option = 0
        self.show_options = False

    def start_dialogue(self, dialogue):
        self.current_dialogue = dialogue
        self.current_line_index = 0
        self.current_typed_text = ""
        self.last_update_time = pygame.time.get_ticks()
        self.finished_typing = False
        self.options = []
        self.selected_option = 0
        self.show_options = False


    def update(self):
        if self.current_dialogue is None:
            return

        current_time = pygame.time.get_ticks()
        if not self.finished_typing:
            if current_time - self.last_update_time > 1000 / self.typing_speed:
                if self.current_line_index < len(self.current_dialogue):
                    current_line = self.current_dialogue[self.current_line_index]
                    
                    #Si es una lista es una opcion
                    if isinstance(current_line, list):
                        self.show_options = True
                        self.options = current_line
                        self.finished_typing = True
                        return

                    #Si tiene opciones
                    if isinstance(current_line, dict) and "options" in current_line:
                        self.show_options = True
                        self.options = current_line["options"]
                        self.finished_typing = True
                        self.current_typed_text = current_line["text"] #Texto antes de opciones
                        return

                    #Texto normal
                    if self.current_typed_text != current_line:
                        self.current_typed_text += current_line[len(self.current_typed_text)]
                        self.last_update_time = current_time
                    else:
                        self.finished_typing = True
        
        #Seleccion de opciones con teclado
        if self.show_options:
            keys = pygame.key.get_pressed()
            if keys[pygame.K_UP]:
                self.selected_option = (self.selected_option - 1) % len(self.options)
                pygame.time.delay(150)  # Evita que se mueva demasiado rápido
            elif keys[pygame.K_DOWN]:
                self.selected_option = (self.selected_option + 1) % len(self.options)
                pygame.time.delay(150)
            elif keys[pygame.K_RETURN]:  # Enter para seleccionar
                self.handle_option_selected()


    def handle_option_selected(self):
        #Avanza a la siguiente linea
        self.current_line_index += 1
        self.current_typed_text = ""
        self.last_update_time = pygame.time.get_ticks()
        self.finished_typing = False
        self.options = []
        self.selected_option = 0
        self.show_options = False

    def draw(self):
        if self.current_dialogue is None:
            return

        pygame.draw.rect(self.screen, self.dialogue_box_color, self.dialogue_box_rect)

        if not self.show_options:
            text_surface = self.font.render(self.current_typed_text, True, self.text_color)
            text_rect = text_surface.get_rect(topleft=(self.dialogue_box_rect.x + 20, self.dialogue_box_rect.y + 20))
            self.screen.blit(text_surface, text_rect)

            if self.finished_typing:
                #Muestra texto para "continuar"
                continue_text = self.font.render("Presiona ENTER para continuar...", True, self.text_color)
                continue_rect = continue_text.get_rect(bottomright=(self.dialogue_box_rect.right - 20, self.dialogue_box_rect.bottom - 10))
                self.screen.blit(continue_text, continue_rect)
        else:
            # Dibuja las opciones
            option_y = self.dialogue_box_rect.y + 20
            for i, option in enumerate(self.options):
                text = option  # En este caso, la opción es solo texto
                color = (255, 255, 0) if i == self.selected_option else self.text_color
                text_surface = self.font.render(text, True, color)
                text_rect = text_surface.get_rect(topleft=(self.dialogue_box_rect.x + 50, option_y))
                self.screen.blit(text_surface, text_rect)
                option_y += 30

    def next_line(self):
        if self.current_dialogue and self.finished_typing and not self.show_options:
            self.current_line_index += 1
            if self.current_line_index < len(self.current_dialogue):
                self.current_typed_text = ""
                self.finished_typing = False
                self.last_update_time = pygame.time.get_ticks()
            else:
                self.current_dialogue = None  # Termina el diálogo

