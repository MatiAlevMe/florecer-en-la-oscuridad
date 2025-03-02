                                                                                                                                                        
 using UnityEngine;                                                                                                                                      
 using UnityEngine.UI;                                                                                                                                   
                                                                                                                                                         
 namespace Components                                                                                                                                    
 {                                                                                                                                                       
     public class ProgressIndicator : MonoBehaviour                                                                                                      
     {                                                                                                                                                   
         public Slider progressBar;                                                                                                                      
         public Image progressImage;                                                                                                                     
         public Color startColor = Color.red;                                                                                                            
         public Color endColor = Color.green;                                                                                                            
         private float progress = 0f;                                                                                                                    
                                                                                                                                                         
         public void UpdateProgress(float amount)                                                                                                        
         {                                                                                                                                               
             progress += amount;                                                                                                                         
             if (progress > 1) progress = 1f;                                                                                                            
                                                                                                                                                         
             progressBar.value = progress;                                                                                                               
             progressImage.color = Color.Lerp(startColor, endColor, progress);                                                                           
         }                                                                                                                                               
     }                                                                                                                                                   
 }                                                                                                                                                       